import os
import sys

CURRENT_DIRECTORY = os.path.dirname(sys.argv[0])
TRAIN_DATA = CURRENT_DIRECTORY + "/trainData"
TEST_DATA = CURRENT_DIRECTORY + "/testData"
OUTPUT_FILE = CURRENT_DIRECTORY + "/result.json"
MODEL_PATH = CURRENT_DIRECTORY + "/trainedModel/model"
CATEGORIES_PATH = CURRENT_DIRECTORY + "/trainedModel/categories"
