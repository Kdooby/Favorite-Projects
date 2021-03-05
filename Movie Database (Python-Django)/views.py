from django.shortcuts import render, redirect, get_object_or_404
from django.http import HttpResponse, HttpResponseRedirect
from .forms import MovieList
from .models import Movies
from bs4 import BeautifulSoup
from bs4 import BeautifulSoup
import requests  # requests information from website
import time
import csv

global context,movie


def home(request):
    return render(request, 'movies/home.html')


def box_office_movies(request, ):
    # provides the text from html page

    box_office_text = requests.get('https://www.imdb.com/chart/boxoffice/?ref_=nv_ch_cht').text
    # variable = instance(object, parser method)
    soup = BeautifulSoup(box_office_text, 'html.parser')
    global context, movie
    # Gets the Current Weekend
    weekend_of = soup.find('h4').text
    print('')

    box_office = []
    # iterate through.   "class_" is used in Beautiful Soup to not be confused with Python's "class"
    movies = soup.find_all('td', class_='posterColumn')
    for movie in movies:
        movie['img'] = movie.img['src']
        movie['title'] = movie.title('td', class_='titleColumn').text
        movie['weekend'] = movie.weekend('td', class_='ratingColumn').text
        movie['gross'] = movie.gross('span', class_='secondaryInfo').text
        movie['weeks'] = movie.weeks('td', class_='weeksColumn').text
    box_office.append(movie)

    return render(request, 'movies/home.html', box_office)


if __name__ == '__main__':
    while True:
        box_office_movies()
        time.sleep(43200)  # refreshes every 12 hours


def all_movies(request):
    movies = Movies.addMovies.all().order_by('Movie_Title')  # variable = Model.modelmanager.allitems
    context = {'movies': movies}  # variable = 'key': value
    return render(request, 'movies/all-movies.html', context)


def add_movies(request):
    form = MovieList(data=request.POST or None)
    if request.method == 'POST':  # If the form meets criteria, proceed to save the data
        if form.is_valid():
            form.save()
            form = MovieList()
    context = {'form': form}
    return render(request, 'movies/add-movies.html', context)


def summary(request, id):
    movie = get_object_or_404(Movies, id=id)
    context = {'movie': movie}
    return render(request, "movies/summary.html", context)


def details(request, id):
    movie = get_object_or_404(Movies, id=id)
    form = MovieList(data=request.POST or None, instance=movie)
    if request.method == 'POST':  # If the form meets criteria, proceed to save the data
        if form.is_valid():
            form.save()
            return redirect('all-movies')
    else:
        context = {'form': form, 'movie': movie}
        return render(request, 'movies/update-movie.html', context)


def delete(request, id):
    delete_movie = get_object_or_404(Movies, id=id)
    if request.method == 'POST':
        delete_movie.delete()
        return redirect('all-movies')
    context = {'delete_movie': delete_movie, }
    return render(request, "movies/confirm-delete.html", context)
