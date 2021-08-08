# python -m pip install --upgrade
# pip install bs4 

import sys
from urllib.parse import quote as urlEncode
from urllib.request import urlopen as uReq
from bs4 import BeautifulSoup as soup

kanji = str(sys.argv[1])
jisho_url = 'https://jisho.org/search/{kanji}%20%23kanji'.format(kanji = kanji)

uClient = uReq(jisho_url)
page_html = uClient.read()
uClient.close()
page_soup = soup(page_html, 'html.parser')

element = page_soup.find("div", { "class": "kanji-details__main-meanings" })
meanings = element.text.replace("\n", "").lstrip(" ").rstrip(" ")
print(meanings)

readings = page_soup.findAll("dd", { "class": "kanji-details__main-readings-list" })
for reading in readings:
    print(urlEncode(reading.text))
