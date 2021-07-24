# pip install bs4           | outside of script
# python -m pip install --upgrade  | consider

import bs4
from urllib.request import urlopen as uReq
from bs4 import BeautifulSoup as soup


my_url = 'https://jisho.org/search/%E5%87%BA%20%23kanji'
# need html encoding for that

uClient = uReq(my_url)
page_html = uClient.read()
uClient.close()
page_soup = soup(page_html, 'html.parser')
meaning = page_soup.findAll("div",{"class":"kanji-details__main-meanings"}) #div.div.a ..
print(meaning[0].text)
