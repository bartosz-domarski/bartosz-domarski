from django.db import models

class Page1(models.Model):
    visits_count = models.IntegerField(default=0)

class Page2(models.Model):
    visits_count = models.IntegerField(default=0)

class Page3(models.Model):
    visits_count = models.IntegerField(default=0)

class ArithmeticAverage(models.Model):
    average = models.IntegerField(default=0)