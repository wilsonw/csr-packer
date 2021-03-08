from collections import OrderedDict
import gzip
import json
import logging
import os

ONLINE_KEY = "UDMZr24F" # not sure when this is used but csrpacker always use offline key
OFFLINE_KEY = "4cPw3ZyC"

VALID_FILE_NAMES = ("nsb", "scb", "trb")

logger = logging.getLogger(__name__)

def unpackFiles(path="", outputPath=""):
    if not outputPath:
        outputPath = os.path.join(path, "unpacked")
    os.makedirs(outputPath, exist_ok=True)
    for filename in VALID_FILE_NAMES:
        try:
            with gzip.open(os.path.join(path, filename), 'rb') as f:
                _hash, content = f.readlines()

                if content[-1] == 0: # apple
                    content = content[:-1]

                data = json.loads(content, object_pairs_hook=OrderedDict)
                outputFileName = os.path.join(outputPath, filename + '.txt')
                
                try:
                    with open(outputFileName, 'w+') as outputFile:
                        json.dump(data, outputFile, indent=4, sort_keys=False)
                        logger.info('Converted %s', filename)
                except:
                    logger.error("Failed to generate output %s", outputFileName, exc_info=True)
        except (IOError, FileNotFoundError):
            logger.info("Ignore %s, not found", filename)
            
def packFiles(path="", outputPath=""):
    import hmac
    import binascii

    if not outputPath:
        outputPath = os.path.join(path, "packed")
    os.makedirs(outputPath, exist_ok=True)
    for filename in VALID_FILE_NAMES:
        try:
            with open(os.path.join(path, filename + '.txt'), 'r') as f:
                data = json.load(f, object_pairs_hook=OrderedDict)
                minimizedString = json.dumps(data, separators=(',', ':'))
                hashString = hmac.digest(bytes(OFFLINE_KEY, 'utf-8'), bytes(minimizedString, 'utf-8'), 'sha1')
                hashString = binascii.hexlify(hashString)
                logger.debug("Hash for %s is %s", filename, hashString)
            try:
                with gzip.open(os.path.join(outputPath, filename), 'w+b') as outputFile:
                    outputFile.write(hashString)
                    outputFile.write(b'\n')
                    outputFile.write((bytes(minimizedString, 'utf-8')))
                    outputFile.write(b'\0')
                    logger.info('Generated %s', filename)
            except:
                logger.error("failed to generate output %s", filename, exc_info=True)
        except (IOError, FileNotFoundError):
            logger.info("Ignore %s.txt, not found", filename)

