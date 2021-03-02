from collections import OrderedDict
import gzip
import json
import os

ONLINE_KEY = "UDMZr24F" # not sure when this is used but csrpacker always use offline key
OFFLINE_KEY = "4cPw3ZyC"

VALID_FILE_NAMES = ("nsb", "scb", "trb")

def unpackFiles(path="/Users/wilsonwu/Projects/temp", outputPath=""):
    for filename in VALID_FILE_NAMES:
        try:
            with gzip.open(os.path.join(path, filename), 'rb') as f:
                _hash, content = f.readlines()

                if content[-1] == 0: # apple
                    content = content[:-1]

                data = json.loads(content, object_pairs_hook=OrderedDict)
                
                if not outputPath:
                    outputPath = os.path.join(path, "output")
                outputFileName = os.path.join(outputPath, filename + '.txt')
                os.makedirs(outputPath, exist_ok=True)
                
                try:
                    with open(outputFileName, 'w+') as outputFile:
                        json.dump(data, outputFile, indent=4, sort_keys=False)
                        print('Converted {}'.format(filename))
                except:
                    print("Failed to generate output {}".format(outputFileName))
        except (IOError, FileNotFoundError):
            print("Ignore {}, not found".format(filename))

unpackFiles()