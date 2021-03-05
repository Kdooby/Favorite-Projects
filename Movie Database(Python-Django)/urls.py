from django.urls import path
from . import views


urlpatterns = [
    path('/', views.home, name='home'),
    path('all-movies/', views.all_movies, name='all-movies'),
    path('add-movies/', views.add_movies, name='add-movies'),
    path('summary/<int:id>/', views.summary, name='summary'),
    path('update-movie/<int:id>/', views.details, name='update-movie'),
    path('confirm-delete/<int:id>/', views.delete, name='confirm-delete'),

]

