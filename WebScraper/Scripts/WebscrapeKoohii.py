# pip install bs4 
# pip install requests 

import sys
import requests
import time
from bs4 import BeautifulSoup as soup

kanji = str(sys.argv[1])
username = str(sys.argv[2])
password = str(sys.argv[3])

koohiiLoginUrl = 'https://kanji.koohii.com/login'
koohiiKanjiUrl = 'https://kanji.koohii.com/study/kanji/{kanji}'.format(kanji = kanji)

session = requests.Session()
session.headers = { "User-Agent": "Chrome/42.0.2311.135" }
credentials = { 'username': username, 'password': password }

attemptGet = True
while attemptGet:
    try:
        session.post(koohiiLoginUrl, data = credentials)
        s = session.get(koohiiKanjiUrl)
        pageSoup = soup(s.text, 'html.parser')

        title = pageSoup.find("title")
        if title.text == 'Oops, please retry in a short moment':
            attemptGet = True
            time.sleep(2)
        elif title.text == 'Sign In - Kanji Koohii':
            print('UNAUTHORIZED')
            exit()
        else:
            attemptGet = False
    except:
        attemptGet = True
        time.sleep(3)

meaning = pageSoup.find("span", { "class": "JSEditKeyword" })
heisingId = pageSoup.find("div", { "class": "framenum" })

print(meaning.text)
print(heisingId.text)