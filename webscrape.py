# pip install bs4 
# python -m pip install --upgrade

import sys
import bs4
import urllib
from urllib.request import urlopen as uReq
from bs4 import BeautifulSoup as soup

my_url = str(sys.argv[1])

uClient = uReq(my_url)
page_html = uClient.read()
uClient.close()
page_soup = soup(page_html, 'html.parser')

elements = page_soup.findAll("div",{"class":"kanji-details__main-meanings"})
meanings = elements[0].text.replace("\n","").lstrip(" ")
print(meanings)

readings = page_soup.findAll("dd",{"class":"kanji-details__main-readings-list"})
for reading in readings:
    print(urllib.parse.quote(reading.text))
