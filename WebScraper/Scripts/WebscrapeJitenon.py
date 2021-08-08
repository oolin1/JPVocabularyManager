# pip install bs4 

import sys
import re
from urllib.parse import quote as urlEncode
from urllib.request import urlopen as uReq
from bs4 import BeautifulSoup as soup

kanji = str(sys.argv[1])
jitenon_url = 'https://jitenon.com/kanji/{kanji}'.format(kanji = kanji)

uClient = uReq(jitenon_url)
page_html = uClient.read()
uClient.close()
page_soup = soup(page_html, 'html.parser')

elements = page_soup.find("ul", { "class": "kanji_data" })
anchors = elements.findAll("a")

regex = "//jitenon.com/parts/(.*)"
compiled = re.compile(regex)
for anchor in anchors:
    if compiled.search(str(anchor)) is not None:
        print(urlEncode(anchor.text))
