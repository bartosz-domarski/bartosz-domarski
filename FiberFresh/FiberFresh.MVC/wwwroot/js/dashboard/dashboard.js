const Helpers = function () {
    this.getTimeString = (dateOfService) => {
        var time = ""

        if (dateOfService.TimeOfDay === 0) {
            time = "Rano"
        } else {
            time = "Wieczór"
        }
        return time
    }

    this.getTimeInt = (TimeOfDay) => {
        var time = ""

        if (TimeOfDay === "Rano") {
            time = 0
        } else {
            time = 1
        }
        return time
    }

    this.dayToString = (day) => {
        var dayToString = ""

        if (day === 0) {
            dayToString = "Niedziela"
        } else if (day === 1) {
            dayToString = "Poniedziałek"
        } else if (day === 2) {
            dayToString = "Wtorek"
        } else if (day === 3) {
            dayToString = "Środa"
        } else if (day === 4) {
            dayToString = "Czwartek"
        } else if (day === 5) {
            dayToString = "Piątek"
        } else if (day === 6) {
            dayToString = "Sobota"
        }
        return dayToString
    }

    this.monthToString = (month) => {
        var monthToString = ""

        if (month === 0) {
            monthToString = "Styczeń"
        } else if (month === 1) {
            monthToString = "Luty"
        } else if (month === 2) {
            monthToString = "Marzec"
        } else if (month === 3) {
            monthToString = "Kwiecień"
        } else if (month === 4) {
            monthToString = "Maj"
        } else if (month === 5) {
            monthToString = "Czerwiec"
        } else if (month === 6) {
            monthToString = "Lipiec"
        } else if (month === 7) {
            monthToString = "Sierpień"
        } else if (month === 8) {
            monthToString = "Wrzesień"
        } else if (month === 9) {
            monthToString = "Październik"
        } else if (month === 10) {
            monthToString = "Listopad"
        } else if (month === 11) {
            monthToString = "Grudzień"
        }
        return monthToString
    }
}
const helpers = new Helpers()

document.getElementById("date").min = new Date().toISOString().split('T')[0]

const dateOfservicePollTable = document.getElementById("dateOfServicePool")

var dateOfServicePool = [];

fetch("Dashboard/GetDates", {
    method: "GET"
})
    .then(response => response.json())
    .then(data => {
        //console.log(data)

        JSON.parse(data).forEach(dateOfService => {
            dateOfServicePool.push(dateOfService)
        })

        if (dateOfServicePool.length > 0) {

            dateOfServicePool.forEach((dateOfService) => {
                const date = new Date(dateOfService.Date)

                var row = dateOfservicePollTable.insertRow(-1)

                var index = dateOfServicePool.indexOf(dateOfService)
                if (index === 0) {
                    row.classList.add("clickedrow")
                }

                var cell1 = row.insertCell(0)
                var cell2 = row.insertCell(1)

                cell1.innerHTML = helpers.dayToString(date.getDay())
                    + " " + dateOfService.Date.split("-")[2]
                    + " " + helpers.monthToString(date.getMonth())

                cell2.innerHTML = helpers.getTimeString(dateOfService)
            })
        }
    })
    .catch(error => {
        //console.log(error)
    })

dateOfservicePollTable.addEventListener("click", event => {
    $(event.target).closest('table').find('tr.clickedrow').removeClass('clickedrow')
    $(event.target).parent('tr').addClass("clickedrow")
})

document.getElementById("remove").addEventListener("click", () => {
    const row = dateOfservicePollTable.querySelector(".clickedrow")

    const date = row.cells[0].innerHTML
    const timeOfDay = row.cells[1].innerHTML

    row.remove()

    const formData = new FormData()
    formData.append("Date", date)
    formData.append("TimeOfDay", helpers.getTimeInt(timeOfDay))

    fetch("Dashboard/DeleteDate", {
        method: "DELETE",
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            //console.log(data)
        })
        .catch(error => {
            //console.log(error)
        })
})