# Generated by Django 2.2.5 on 2021-01-29 01:52

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('movies', '0003_auto_20210128_1334'),
    ]

    operations = [
        migrations.AddField(
            model_name='movies',
            name='Movie_Summary',
            field=models.TextField(blank=True, default='', max_length=5000),
        ),
    ]
