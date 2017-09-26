import os
import sys

CURRENT_DIRECTORY = os.path.dirname(sys.argv[0])
PHOTO_PATH = CURRENT_DIRECTORY + "/trainData"
INPUT_FILE = CURRENT_DIRECTORY + "/image.jpg"
OUTPUT_FILE = CURRENT_DIRECTORY + "/result.txt"
MODEL_PATH = CURRENT_DIRECTORY + "/trainedModel/model"

MINIMUM_PHOTOS = 5