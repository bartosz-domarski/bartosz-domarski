const Helpers = function () {
    this.getTime = (dateOfService) => {
        var time = ""

        if (dateOfService.TimeOfDay === 0) {
            time = "Rano"
        } else {
            time = "Wieczór"
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

const Loadings = function () {
    const loaderValuation = document.getElementById("loadingValuation")
    const loaderSubmit = document.getElementById("loadingSubmit")

    this.valuationShow = () => {
        loaderValuation.classList.add("active")
    }

    this.valuationHide = () => {
        loaderValuation.classList.remove("active");
    }

    this.submitShow = () => {
        loaderSubmit.classList.add("active")
    }

    this.submitHide = () => {
        loaderSubmit.classList.remove("active");
    }
}
const loadings = new Loadings()

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
            var tbody = document.getElementById("dateOfServicePool").querySelector("tbody")

            dateOfServicePool.forEach((dateOfService) => {
                const date = new Date(dateOfService.Date)

                var row = tbody.insertRow(-1)

                var index = dateOfServicePool.indexOf(dateOfService)
                if (index === 0) {
                    row.classList.add("clickedrow")
                }

                var cell1 = row.insertCell(0)
                var cell2 = row.insertCell(1)

                cell1.innerHTML = helpers.dayToString(date.getDay())
                    + " " + dateOfService.Date.split("-")[2]
                    + " " + helpers.monthToString(date.getMonth())

                cell2.innerHTML = helpers.getTime(dateOfService)
            })
        }
    })
    .catch(error => {
        //console.log(error)
    })

const multiStepForm = document.querySelector("[data-multi-step]")
const formSteps = [...multiStepForm.querySelectorAll("[data-step]")]
var services = [...multiStepForm.querySelectorAll('#myTable > tbody > tr[name="service"]')]

var currentStep = formSteps.findIndex(step => {
    return step.classList.contains("active")
})

if (currentStep < 0) {
    currentStep = 0
    showCurrentStep()
}

multiStepForm.addEventListener("click", async event => {
    var incrementor
    if (event.target.matches("[data-next]")) {
        incrementor = 1
    } else if (event.target.matches("[data-previous]")) {
        incrementor = -1
    }

    if (incrementor == null) return

    const inputs = [...formSteps[currentStep].querySelectorAll("input")]
    const allValid = inputs.every(input => input.reportValidity())

    if (allValid && checkIfAtLeastOneService() && event.target.matches("[data-next]") && currentStep === 2) {
        const imageElements = document.getElementsByName("image")
        var isOk = true
        imageElements.forEach(e => {
            const images = e.querySelectorAll("img")

            if (images.length < 2 && isOk) {
                alert("Proszę dodać przynajmniej po 2 zdjęcia dla każdego mebla.")
                incrementor = 0
                isOk = false
            }
        })
        currentStep += incrementor
        showCurrentStep()
    } else if (allValid && checkIfAtLeastOneService() && event.target.matches("[data-next]")) {
        currentStep += incrementor
        showCurrentStep()
    } else if (event.target.matches("[data-previous]")) {
        currentStep += incrementor
        showCurrentStep()
    }
})

function showCurrentStep() {
    formSteps.forEach((step, index) => {
        step.classList.toggle("active", index === currentStep)
    })
}

const furnitureElement = document.getElementById("selectFurniture")
furnitureElement.addEventListener('change', (e) => {
    document.getElementById("cbm").classList.toggle("active", e.target.value === "Carpet" || furnitureElement.value === "Facing")
    document.getElementById("size").classList.toggle("active", e.target.value != "Carpet" && furnitureElement.value != "Facing")
})

const fabricNotAvailable = ["FauxLeather", "GenuineLeather", "Carabu", "Viscose", "Penta", "Jute"]
const fabricElement = document.getElementById("selectFabric")

fabricElement.addEventListener('change', (e) => {
    const infobox = document.getElementById("infobox")

    infobox.classList.toggle("active", e.target.value === "FauxLeather" || e.target.value === "GenuineLeather"
        || e.target.value === "Carabu" || e.target.value === "Viscose" || e.target.value === "Penta" || e.target.value === "Jute")

    infobox.innerHTML = "<p class='title'>Ważne!</p>Wybrany materiał nie nadaje się do prania ekstrakcyjnego lub efekty takiego prania są niezadowalające, dlatego nie podejmuje się prania wybranego materiału."
})

var cbm
document.getElementById("inputCBM").addEventListener('input', (e) => {
    cbm = parseFloat(e.target.value)
})

function Add() {
    if (!cbm && (furnitureElement.value === "Carpet" || furnitureElement.value === "Facing")) {
        return alert('Proszę wypełnić wszystkie pola.');
    }

    if (fabricElement.value === "FauxLeather" || fabricElement.value === "GenuineLeather"
        || fabricElement.value === "Carabu" || fabricElement.value === "Viscose" || fabricElement.value === "Penta" || fabricElement.value === "Jute") {
        return alert('Nie podejmuje się prania wybranego materiału.');
    }

    const furniturePlaceholder = furnitureElement.value
    const furnitureValue = furnitureElement.options[furnitureElement.selectedIndex].text

    const fabric = document.getElementById("selectFabric")
    const fabricPlaceholder = fabric.value
    const fabricValue = fabric.options[fabric.selectedIndex].text

    const size = document.getElementById("selectSize")
    const sizePlaceholder = size.value
    const sizeValue = size.options[size.selectedIndex].text

    const sizeOrCBM = cbm ?? sizeValue

    if (services.length == 0) {
        $('#myTable > thead').append('<tr><th scope="col">Przedmiot</th>' +
            '<th scope="col">Materiał</th>' +
            '<th scope="col">Pow./Wiel.</th></tr>')
    }

    $('#myTable > tbody').append('<tr name="service"><td><input disabled name="furniture" class="form-control" required="required" placeholder="' + furniturePlaceholder + '" value="' + furnitureValue + '" /><span class="field-validation-valid text-danger" data-valmsg-for="Furniture" data-valmsg-replace="true"></span></td>' +
        '<td> <input disabled name="fabric" class="form-control"  required="required"  placeholder="' + fabricPlaceholder + '" value="' + fabricValue + '"/><span class="field-validation-valid text-danger" data-valmsg-for="Fabric" data-valmsg-replace="true"></span></td>' +
        '<td> <input disabled name="sizeOrCBM" class="form-control"  required="required"  placeholder="' + sizePlaceholder + '" value="' + sizeOrCBM + '"/><span class="field-validation-valid text-danger" data-valmsg-for="sizeOrCBM" data-valmsg-replace="true"></span></td>' +
        '<td><input type="button" onclick="Delete(this)" class="btn btn-danger" value="Usuń" /></td ></tr>')

    services = [...multiStepForm.querySelectorAll('#myTable > tbody > tr[name="service"]')]

    cbm = null
}

function Delete(d) {
    $(d).closest("tr").remove()

    services = [...multiStepForm.querySelectorAll('#myTable > tbody > tr[name="service"]')]

    if (services.length === 0) {
        $('#myTable > thead > tr').remove()
    }
}

function checkIfAtLeastOneService() {
    if ($('#myTable > tbody').find("input").length) {
        return true
    } else {
        return false
    }
}

$('#modal').on('hidden.bs.modal', (e) => {
    $(e.target).find("input").val('')
})

document.getElementById("uploadImagesButton").addEventListener("click", event => {
    if ($('#uploadImagesTable').find("tr[name='service']").length != services.length) {
        $('#uploadImagesTable').html('')

        services.forEach(service => {

            const furniture = service.querySelector('input[name="furniture"]').value
            const fabric = service.querySelector('input[name="fabric"]').value

            const index = services.indexOf(service)

            $('#uploadImagesTable').append('<tr name="service"><td><input disabled name="furniture" class="form-control" required="required" value="' + furniture + '" /><span class="field-validation-valid text-danger" data-valmsg-for="Furniture" data-valmsg-replace="true"></span></td>' +
                '<td> <input disabled name="fabric" class="form-control"  required="required" value="' + fabric + '"/><span class="field-validation-valid text-danger" data-valmsg-for="Fabric" data-valmsg-replace="true"></span></td>' +
                '<td> <input type="file" multiple class="inputFile" accept="image/*" onchange="readMultipleFiles(event, image' + index + '.id)"></td></tr>' +
                '<tr name="image"> <td colspan="100"><div id="image' + index + '"></div></td></tr>')
        })
    }
})

const readMultipleFiles = (event, id) => {
    const file = [...event.target.files] || []

    file.forEach((item, index) => {
        const reader = new FileReader()

        reader.onloadend = () => {
            const outputDiv = document.getElementById(id)

            const img = document.createElement("img")
            img.classList.add("img")
            img.src = reader.result
            img.onclick = function () { $(this).remove() }
            outputDiv.appendChild(img)
        }
        reader.readAsDataURL(file[index])
    })
}

document.getElementById("dateOfServicePool").addEventListener("click", event => {
    $(event.target).closest('table').find('tr.clickedrow').removeClass('clickedrow')
    $(event.target).parent('tr').addClass("clickedrow")
})

document.getElementById("valuationButon").addEventListener("click", event => {
    loadings.valuationShow()

    const formData = new FormData(multiStepForm)

    if (services.length === 0) {
        return alert('Proszę dodać przynajmniej jedną usługę.');
    }

    services.forEach(service => {

        const furniture = service.querySelector('input[name="furniture"]').getAttribute('placeholder')
        const fabric = service.querySelector('input[name="fabric"]').getAttribute('placeholder')
        const cbm = service.querySelector('input[name="sizeOrCBM"]').value
        const size = service.querySelector('input[name="sizeOrCBM"]').getAttribute('placeholder')

        const index = services.indexOf(service)

        formData.append("Services[" + index + "].Furniture", furniture)
        formData.append("Services[" + index + "].Fabric", fabric)
        formData.append("Services[" + index + "].CBM", cbm.replace(".", ","))
        formData.append("Services[" + index + "].Size", size)
    })

    formData.delete("FirstName")
    formData.append("FirstName", "Valuation")

    fetch("Create", {
        method: "POST",
        body: formData
    })
        .then(response => response.json())
        .then(valuation => {
            loadings.valuationHide()
            multiStepForm.querySelector('.valuation').innerHTML = valuation["valuation"] + " PLN"
        })
        .catch(error => {
            //console.log(error)
        })
})

multiStepForm.onkeypress = function (e) {
    var key = e.charCode || e.keyCode || 0;
    if (key == 13) {
        e.preventDefault();
    }
}

function b64ToBlob(b64Data, contentType, sliceSize) {
    contentType = contentType || "";
    sliceSize = sliceSize || 512;

    var byteCharacters = atob(b64Data);
    var byteArrays = [];

    for (
        var offset = 0;
        offset < byteCharacters.length;
        offset += sliceSize
    ) {
        var slice = byteCharacters.slice(offset, offset + sliceSize);

        var byteNumbers = new Array(slice.length);
        for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        var byteArray = new Uint8Array(byteNumbers);

        byteArrays.push(byteArray);
    }

    var blob = new Blob(byteArrays, { type: contentType });
    return blob;
}

function imageToBlob(base64Image) {
    var block = base64Image.split(";")
    var contentType = block[0].split(":")[1]
    var realData = block[1].split(",")[1]

    return b64ToBlob(realData, contentType);
}

multiStepForm.addEventListener("submit", event => {
    event.preventDefault()

    loadings.submitShow()

    const formData = new FormData(multiStepForm)

    const services = [...multiStepForm.querySelectorAll('#myTable > tbody > tr[name="service"]')]
    const imageElements = [...multiStepForm.querySelectorAll('#uploadImagesTable > tr[name="image"]')]

    const servicesSummary = document.getElementById("ServicesSummary")

    services.forEach(service => {

        const furniture = service.querySelector('input[name="furniture"]')
        const fabric = service.querySelector('input[name="fabric"]')
        var cbm = service.querySelector('input[name="sizeOrCBM"]').value.replace(".", ",")
        const size = service.querySelector('input[name="sizeOrCBM"]')

        if (cbm === "Małe" || cbm === "Średnie" || cbm === "Duże") {
            cbm = 0
        }

        const indexService = services.indexOf(service)

        formData.append("Services[" + indexService + "].Furniture", furniture.getAttribute('placeholder'))
        formData.append("Services[" + indexService + "].Fabric", fabric.getAttribute('placeholder'))
        formData.append("Services[" + indexService + "].CBM", cbm)
        formData.append("Services[" + indexService + "].Size", size.getAttribute('placeholder'))

        const images = [...imageElements[indexService].querySelectorAll("img")]
        images.forEach(image => {
            formData.append("Services[" + indexService + "].Images[" + images.indexOf(image) + "].Name", "Blob")
            formData.append("Services[" + indexService + "].Images[" + images.indexOf(image) + "].Blob", imageToBlob(image.src))
        })

        const labelServiceSummary = document.createElement("label")
        var sizeOrCBM = ""
        if (cbm === 0) {
            sizeOrCBM = size.value
        } else {
            sizeOrCBM = cbm
        }
        labelServiceSummary.textContent = furniture.value + " | " + fabric.value + " | " + sizeOrCBM
        servicesSummary.appendChild(labelServiceSummary)
    })

    const rows = [...document.getElementById("dateOfServicePool").querySelector("tbody").querySelectorAll("tr")]
    const currentServiceDate = document.getElementById("dateOfServicePool").querySelector('tr.clickedrow')
    const indexRow = rows.indexOf(currentServiceDate)

    formData.append("DateOfService", dateOfServicePool[indexRow].Date)
    formData.append("TimeOfDay", dateOfServicePool[indexRow].TimeOfDay)

    formData.delete("__Invariant")
    formData.delete("IsElevator")
    formData.append("IsElevator", document.getElementsByClassName("form-check-input")[0].checked)

    fetch("Create", {
        method: "POST",
        body: formData
    })
        .then(response => {
            loadings.submitHide()
            if (response.status == 202) {
                document.getElementById("FullNameSummary").innerHTML += "<b>" + formData.get("FirstName") + " " + formData.get("LastName") + "</b>"
                document.getElementById("ValuationSummary").innerHTML += "<b>" + multiStepForm.querySelector('.valuation').innerHTML + "</b>"
                document.getElementById("AddressSummary").innerHTML += "<b>" + formData.get("City") + ", " + formData.get("Street") + "</b>"
                document.getElementById("PhoneNumberSummary").innerHTML += "<b>" + formData.get("PhoneNumber") + "</b>"
                document.getElementById("EmailSummary").innerHTML += "<b>" + formData.get("Email") + "</b>"
                document.getElementById("FloorSummary").innerHTML += "<b>" + formData.get("Floor") + "</b>"
                document.getElementById("NoteSummary").innerHTML += "<b>" + formData.get("Note") + "</b>"

                var dateOfService = document.getElementById("DateOfServiceSummary")
                dateOfService.innerHTML += "<b>" + formData.get("DateOfService") + ", " + "</b>"

                const timeOfDay = formData.get("TimeOfDay")
                if (timeOfDay === 0) {
                    dateOfService.innerHTML += "<b>" + "Godziny poranne/południowe" + "</b>"
                } else {
                    dateOfService.innerHTML += "<b>" + "Godziny popołudniowe/wieczorne" + "</b>"
                }

                var isElevatorSummary = document.getElementById("IsElevatorSummary"),
                    isElevator = formData.get("IsElevator")

                if (isElevator === "true") {
                    isElevatorSummary.innerHTML += "<b>" + "Tak" + "</b>"
                } else {
                    isElevatorSummary.innerHTML += "<b>" + "Nie" + "</b>"
                }

                $("#modalSummary").modal('show')
                $('#modalSummary').on('hidden.bs.modal', () => {
                    window.location.replace(window.location.origin)
                })

            } else {
                alert("Coś poszło nie tak, proszę sprawdzić poprawność wprowadzonych danych lub odświeżyć stronę i spróbować jeszcze raz.")
            }
        })
        .then(data => {
            //console.log(data)
        })
        .catch(error => {
            //console.log(error)
        })
})