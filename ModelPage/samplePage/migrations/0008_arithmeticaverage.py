# Generated by Django 4.1.6 on 2023-02-10 11:31

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('samplePage', '0007_delete_arithmeticaverage'),
    ]

    operations = [
        migrations.CreateModel(
            name='ArithmeticAverage',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('average', models.IntegerField(default=0)),
            ],
        ),
    ]
