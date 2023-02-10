from humanfriendly import format_timespan

def timeCalc(pagesToPrint):
    printer1 = {"pages":50, "minutes":10}
    printer2 = {"pages":50, "minutes":30}
    
    printer1efficiency = printer1["pages"] / printer1["minutes"]
    printer2efficiency = printer2["pages"] / printer2["minutes"]

    printersEfficiencyPagesPerMinute = printer1efficiency + printer2efficiency

    seconds = (pagesToPrint / printersEfficiencyPagesPerMinute) * 60

    return "It will take {} to print {} pages".format(format_timespan(seconds), str(pagesToPrint))

print("How many pages you want to print?")
print(timeCalc(int(input())))