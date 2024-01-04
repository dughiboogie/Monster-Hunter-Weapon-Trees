import os
import json

folderSubpath = "AppData/LocalLow/DughiBoogie/MH Weapon Trees/Data/"
filesList = os.listdir(os.path.join(os.getenv("HOMEPATH"), folderSubpath))

for fileName in filesList:
    print("Updating sharpness values in " + fileName)

    subpath = "AppData/LocalLow/DughiBoogie/MH Weapon Trees/Data/" + fileName

    resultFile = ""

    with open(os.path.join(os.getenv("HOMEPATH"), subpath), "r") as jsonFile:
        parsedJsonFile = json.load(jsonFile)

        for weaponTree in parsedJsonFile["weaponTrees"]:

            for weapon in weaponTree["weapons"]:
                weaponStats = weapon["weaponStats"]

                for stat in weaponStats.keys():
                    if stat == "sharpnesses" or stat == "sharpnessesUpdate" or stat == "sharpnessesMax":

                        if(len(weaponStats[stat]) < 8):
                            print("Adding new " + stat + " entry!")
                            weaponStats[stat].append({ "colour": 7, "value": 0 })

        resultFile = parsedJsonFile
        
    with open(os.path.join(os.getenv("HOMEPATH"), subpath), "w") as outFile:
        json.dump(resultFile, outFile, ensure_ascii=False, indent=2)