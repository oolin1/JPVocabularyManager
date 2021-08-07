# pip install bs4 
# pip install requests 

import sys
import requests

from bs4 import BeautifulSoup as soup

kanji = str(sys.argv[1])
username = str(sys.argv[2])
password = str(sys.argv[3])

koohii_login_url = 'https://kanji.koohii.com/login'
koohii_kanji_url = 'https://kanji.koohii.com/study/kanji/{kanji}'.format(kanji = kanji)

session = requests.Session()
session.headers = { "User-Agent": "Chrome/42.0.2311.135" }
credentials = { 'username': username, 'password': password }

session.post(koohii_login_url, data = credentials)
s = session.get(koohii_kanji_url)
page_soup = soup(s.text, 'html.parser')

meaning = page_soup.findAll("span", { "class": "JSEditKeyword" })
heising_id = page_soup.findAll("div", { "class": "framenum" })

print(meaning[0].text)
print(heising_id [0].text)