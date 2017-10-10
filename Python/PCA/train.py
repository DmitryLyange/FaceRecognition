import os
from PIL import Image
import numpy as np
import random
import constants
import preprocessing
import model
import pickle


def main():
    image_preprocessor = preprocessing.ImagePreprocessor()

    x_load = []
    y_load = []
    categories = dict()
    classes = dict()

    for path in os.listdir(constants.TRAIN_DATA):
        classes[path] = 0
        categories[path] = random.choice([0, 1])
        for fileName in os.listdir(constants.TRAIN_DATA + "//" + path):
            file_path = constants.TRAIN_DATA + "//" + path + "//" + fileName
            image = image_preprocessor.convert_image(Image.open(file_path))
            if not image:
                continue

            image = np.array(image, 'uint8')
            classes[path] += 1
            x_load.append(image)
            y_load.append(path)

    X = []
    Y = []

    for x, y in zip(x_load, y_load):
        #if classes[y] < constants.MINIMUM_PHOTOS:
        #    continue

        X.append(x)
        Y.append(y)

    X, Y = image_preprocessor.filter_images(X, Y)

    face_recognizer = model.FaceRecognizer()
    face_recognizer.fit(X, Y)
    face_recognizer.save(constants.MODEL_PATH)

    with open(constants.CATEGORIES_PATH, 'wb') as file:
        pickle.dump(categories, file)


if __name__ == "__main__":
    main()
