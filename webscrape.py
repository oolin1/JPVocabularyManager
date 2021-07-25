# pip install bs4           | outside of script
# python -m pip install --upgrade  | consider

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

meaning = page_soup.findAll("div",{"class":"kanji-details__main-meanings"})
print(meaning[0].text)

readings = page_soup.findAll("dd",{"class":"kanji-details__main-readings-list"})
for reading in readings:
    print(urllib.parse.quote(reading.text) + '\n')
