import json
import hashlib
import random
import string


file = open("data.json", encoding="utf-8")
js = json.loads(file.read())
file.close()

count = len(js["map"])
# for a in js["map"]:
#     if "Value" in a:
#         massA = a["Value"].split(";")
#         for replic in massA:
#             print(replic)



while True:
    print("Сцена ", len(js["map"]))
    pers = []
    while True:
        personaj_name = input("Имя персонажа: ")
        if(personaj_name == ""):
            break
        pers.append(personaj_name)

    Value = ""
    if(len(pers) == 0):
        inp = input(personaj_name+": ")
        Value = inp
    else:
        key = ""
        while key != "ext":
            for personaj_name in pers:
                inp = input(personaj_name+": ")
                if(inp == "ext"):
                    key = inp
                    break
                if(inp == ""):
                    continue
                if Value != "":
                    Value += ";"+personaj_name+":"+inp
                else:
                    Value += personaj_name+":"+inp

    js["map"].append({"key": ''.join(random.sample(string.ascii_lowercase+string.digits,32)), 'Value':Value})
    with open('data.json', 'w', encoding='utf-8') as file:
        json.dump(js, file, ensure_ascii=False, indent=4)

    if input("К следующему диалогу?(нет-2, да-1) ") == "2":
        break
# print(Value)
# inp = 0
# while(inp != "exit"):
#     inp = input("фраза:")
#     if(inp == "12"):
#         break
#     js["map"].append({"key": ''.join(random.sample(string.ascii_lowercase+string.digits,32)), 'Value':inp})


# with open('data.json', 'w', encoding='utf-8') as file:
    # json.dump(js, file, ensure_ascii=False, indent=4)






# while
