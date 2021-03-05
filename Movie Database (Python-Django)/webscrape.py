from bs4 import BeautifulSoup
import requests  # requests information from website
import time


def box_office_movies():
    # provides the text from html page
    box_office_text = requests.get('https://www.imdb.com/chart/boxoffice/?ref_=nv_ch_cht').text
    # variable = instance(object, parser method)
    soup = BeautifulSoup(box_office_text, 'html.parser')

    # Gets the Current Weekend
    weekend_of = soup.find('h4').text
    print('')
    print(f"{weekend_of}")
    print('')
    # iterate through
    movies = soup.find_all('td', class_='posterColumn')
    for movie in movies:
        poster = movie.find('img')
        title = movie.find('td', class_='titleColumn').text
        weekend = movie.find('td', class_='ratingColumn').text
        gross = movie.find('span', class_='secondaryInfo').text
        weeks = movie.find('td', class_='weeksColumn').text

        print(f"{poster}")
        print(f"Title: {title.strip()}")  # strip gets rid of blank spaces
        print(f"Weekend Gross: {weekend.strip()}")  # strip gets rid of blank spaces
        print(f"Total Gross: {gross}")
        print(f"Weeks in Theaters: {weeks}")
        print('')


if __name__ == '__main__':
    while True:
        box_office_movies()
        time.sleep(43200)  # refreshes every 12 hours
