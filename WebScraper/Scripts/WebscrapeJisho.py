# python -m pip install --upgrade
# pip install bs4 

import sys
import requests
from urllib.parse import quote as urlEncode
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

        print(urlEncode(readings.rstrip("、")))


kanji = str(sys.argv[1])
jishoUrl = "https://jisho.org/search/{kanji}%20%23kanji".format(kanji = kanji)

session = requests.Session()
session.headers = { "User-Agent": "Chrome/42.0.2311.135" }
s = session.get(jishoUrl)
pageSoup = soup(s.text, 'html.parser')

element = pageSoup.find("div", { "class": "kanji-details__main-meanings" })
meanings = element.text.replace("\n", "").lstrip(" ").rstrip(" ")
print(meanings)

kanjiSection = pageSoup.find("div", { "class": "row kanji-details--section" })
printReading(kanjiSection, "dictionary_entry kun_yomi")
printReading(kanjiSection, "dictionary_entry on_yomi")
