# python -m pip install --upgrade
# pip install bs4 

import sys
from urllib.parse import quote as urlEncode
from urllib.request import urlopen as uReq
from bs4 import BeautifulSoup as soup

kanji = str(sys.argv[1])
jishoUrl = "https://jisho.org/search/{kanji}%20%23kanji".format(kanji = kanji)

uClient = uReq(jishoUrl)
pageHtml = uClient.read()
uClient.close()
pageSoup = soup(pageHtml, "html.parser")

element = pageSoup.find("div", { "class": "kanji-details__main-meanings" })
meanings = element.text.replace("\n", "").lstrip(" ").rstrip(" ")
print(meanings)

kanjiSection = pageSoup.find("div", { "class": "row kanji-details--section" })

kunElement = kanjiSection.find("dl", { "class": "dictionary_entry kun_yomi" })
kunReadings = ""
if kunElement is None:
    print(kunReadings)
else:
    kunAnchors = kunElement.findAll("a")
    for anchor in kunAnchors:
        kunReadings = "{readings}{newReading}、".format(readings = kunReadings, newReading = anchor.text)

    print(urlEncode(kunReadings.rstrip("、")))

onElement = kanjiSection.find("dl", { "class": "dictionary_entry on_yomi" })
onReadings = ""
if onElement is None:
    print(onReadings)
else:
    onAnchors = onElement.findAll("a")
    for anchor in onAnchors:
        onReadings = "{readings}{newReading}、".format(readings = onReadings, newReading = anchor.text)

    print(urlEncode(anchor.text).rstrip("、"))
