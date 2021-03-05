from django.db import models
from django.urls import reverse

GENRE_CHOICES = [
    ('Action', 'Action'),
    ('Anime', 'Anime'),
    ('Comedy', 'Comedy'),
    ('Drama', 'Drama'),
    ('Fantasy', 'Fantasy'),
    ('Horror', 'Horror'),
    ('Romance', 'Romance'),
    ('Science Fiction', 'Science Fiction'),
]


class Movies(models.Model):
    Genre = models.CharField(max_length=60, choices=GENRE_CHOICES)
    Movie_Title = models.CharField(max_length=60, default="", blank=True, null=False)
    Starring = models.CharField(max_length=60, default="", blank=True, null=False)
    Directed_By = models.CharField(max_length=60, default="", blank=True, null=False)
    Year_Released = models.IntegerField(default="")
    Movie_Summary = models.TextField(max_length=5000, default="", blank=True, null=False)

    addMovies = models.Manager()

    def __str__(self):
        return self.Movie_Title

