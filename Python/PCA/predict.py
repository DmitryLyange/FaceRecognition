from PIL import Image
import os
import model
import preprocessing
import constants
import numpy as np
import pickle
import json


def main():
    image_preprocessor = preprocessing.ImagePreprocessor()
    face_recognizer = model.FaceRecognizer().load(constants.MODEL_PATH)

    with open(constants.CATEGORIES_PATH, 'rb') as file:
        categories = pickle.load(file)

    x_load = []
    y_load = []
    result = [0] * 12
    tests_number = 0

    for path in os.listdir(constants.TEST_DATA):
        for fileName in os.listdir(constants.TEST_DATA + "//" + path):
            file_path = constants.TEST_DATA + "//" + path + "//" + fileName
            image = image_preprocessor.convert_image(Image.open(file_path))
            if not image:
                continue

            image = np.array(image, 'uint8')
            tests_number += 1
            x_load.append(image)
            y_load.append(path)

    images, image_classes = preprocessing.ImagePreprocessor().filter_images(x_load, y_load)

    for image, image_class in zip(images, image_classes):
        probability = face_recognizer.predict_proba(image)[0]
        if image_class not in categories:
            true_category = 2
        else:
            true_category = categories[image_class]

        if max(probability) >= 0.6:
            answer = probability.index(max(probability))
            answer_class = face_recognizer.get_target_names()[answer]
            predicted_category = categories[answer_class]
            if predicted_category == 0:
                if true_category == 0:
                    if image_class == answer_class:
                        result[1] += 1
                    else:
                        result[2] += 1
                elif true_category == 1:
                    result[3] += 1
                else:
                    result[4] += 1
            elif predicted_category == 1:
                if true_category == 0:
                    result[5] += 1
                elif true_category == 1:
                    if image_class == answer_class:
                        result[6] += 1
                    else:
                        result[7] += 1
                else:
                    result[8] += 1
        elif true_category == 0:
            result[9] += 1
        elif true_category == 1:
            result[10] += 1
        else:
            result[11] += 1

    a = []
    if not os.path.isfile(constants.OUTPUT_FILE):
        a.append(result)
        with open(constants.OUTPUT_FILE, mode='w') as f:
            json.dump(a, f)
    else:
        with open(constants.OUTPUT_FILE) as feeds_json:
            feeds = json.load(feeds_json)

        feeds.append(result)
        with open(constants.OUTPUT_FILE, mode='w') as f:
            json.dump(feeds, f)

if __name__ == "__main__":
    main()
