import json
import sys
import os

path = sys.argv[1]

outjson = {}
newVersion = "" 

with open(path, 'r') as f:
    j = json.load(f)
    s = j["version"]
    spl = s.split(".")
    spl[2] = str(int(spl[2])+1)
    newVersion = ".".join(spl)
    j["version"] = newVersion
    outjson = j

with open(path, 'w') as f:
    json.dump(outjson, f)

# Output
print(newVersion)