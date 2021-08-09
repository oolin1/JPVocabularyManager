# python -m pip install --upgrade
# pip install bs4 

import sys
from urllib.parse import quote as urlEncode
from urllib.request import urlopen as uReq
from bs4 import BeautifulSoup as soup

def printReading(kanjiSection, readingClass):
    element = kanjiSection.find("dl", { "class": readingClass })
    readings = ""
    if element is None:
        print(readings)
    else:
        anchors = element.findAll("a")
        for anchor in anchors:
            readings = "{readings}{newReading}、".format(readings = readings, newReading = anchor.text)

        print(urlEncode(readings).rstrip("、"))


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
printReading(kanjiSection, "dictionary_entry kun_yomi")
printReading(kanjiSection, "dictionary_entry on_yomi")
