# pip install bs4 

import sys
import re
from urllib.parse import quote as urlEncode
from urllib.request import urlopen as uReq
from bs4 import BeautifulSoup as soup

kanji = str(sys.argv[1])
jitenonUrl = 'https://jitenon.com/kanji/{kanji}'.format(kanji = kanji)

uClient = uReq(jitenonUrl)
pageHtml = uClient.read()
uClient.close()
pageSoup = soup(pageHtml, 'html.parser')

elements = pageSoup.find("ul", { "class": "kanji_data" })
anchors = elements.findAll("a")

regex = "//jitenon.com/parts/(.*)"
compiled = re.compile(regex)
for anchor in anchors:
    if compiled.search(str(anchor)) is not None:
        print(urlEncode(anchor.text))
