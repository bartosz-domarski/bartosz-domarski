from django.shortcuts import render
from .models import Page1, Page2, Page3, ArithmeticAverage

def page1(request):
    if Page1.objects.count() <= 0:
        page1 = Page1.objects.create()
        page1.save()
    else:
        page1 = Page1.objects.first()
        page1.visits_count += 1
        page1.save()
    return render(request, 'sample1.html', {"visits_count":page1.visits_count})

def page2(request):
    if Page2.objects.count() <= 0:
        page2 = Page2.objects.create()
        page2.save()
    else:
        page2 = Page2.objects.first()
        page2.visits_count += 1
        page2.save()
    return render(request, 'sample2.html', {"visits_count":page2.visits_count})

def page3(request):
    if Page3.objects.count() <= 0:
        page3 = Page3.objects.create()
        page3.save()
    else:
        page3 = Page3.objects.first()
        page3.visits_count += 1
        page3.save()
    return render(request, 'sample3.html', {"visits_count":page3.visits_count})

def arithmeticAverage(request):
    if ArithmeticAverage.objects.count() <= 0:
        arithmeticAverage = ArithmeticAverage.objects.create()
        arithmeticAverage.save()
    else:
        arithmeticAverage = ArithmeticAverage.objects.get(pk=1)
        arithmeticAverage.average = (Page1.objects.first().visits_count + 
        Page2.objects.first().visits_count + Page3.objects.first().visits_count) / 3
        arithmeticAverage.save()
    return render(request, 'average.html', {'average': arithmeticAverage.average})