# Generated by Django 4.1.6 on 2023-02-10 10:32

from django.db import migrations


class Migration(migrations.Migration):

    dependencies = [
        ('samplePage', '0004_rename_visits_count_page1_visits_count1_and_more'),
    ]

    operations = [
        migrations.RenameField(
            model_name='page1',
            old_name='visits_count1',
            new_name='visits_count',
        ),
        migrations.RenameField(
            model_name='page2',
            old_name='visits_count2',
            new_name='visits_count',
        ),
        migrations.RenameField(
            model_name='page3',
            old_name='visits_count3',
            new_name='visits_count',
        ),
    ]
