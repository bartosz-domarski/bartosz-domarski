function saveElement(selector) {
    let availableSystems = []
    let elementsText = []
    let elementsUrl = []
    let elementsDataID = []
    let elementsDataValue = []
    const elements = document.querySelectorAll(selector)

    elements.forEach(element => {
        if (!element.textContent.includes('Bonus')) {
            elementsText.push(element.getElementsByClassName('ticket-bet-name')[0].textContent),
            elementsUrl.push(element.querySelector('a[href]').getAttribute('href')),
            elementsDataID.push(element.dataset.id),
            elementsDataValue.push(element.getElementsByClassName('odds-value')[0].textContent)
        }
    })

    if (checkLengths(elementsText, elementsUrl, elementsDataID, elementsDataValue)) {
        if (Math.max(...elementsDataValue) <= 3.5 && Math.min(...elementsDataValue) >= 1.2) {
            if (elementsText.length == 7) {
                availableSystems.push('4/7/7')
            } else if (elementsText.length == 8) {
                availableSystems.push('4/8/14')
            } else {
                availableSystems.push('Zła ilość zdarzeń')
            }
        } else {
            availableSystems.push('Złe kursy')
        }

        chrome.storage.local.set({ 'availableSystems': availableSystems, 'elementsText': elementsText, 'elementsUrl': elementsUrl,
            'elementsDataID': elementsDataID, 'elementsDataValue': elementsDataValue }, () => {
            console.log('Elements saved to local storage ' + elementsText.length)
        })
    } else {
        alert('Brakuje danych')
    }
}

const observer = new MutationObserver((mutationsList) => {
    for (let mutation of mutationsList) {
        if (mutation.type === 'childList' && mutation.target.classList.contains('ticket-content')) {
            saveElement("#ticket-content > div.sub-ticket-box > div.ticket-body-wrapper > div > div > div > div > div")
        }
    }
})
  
const targetNode = document.documentElement
const config = { childList: true, subtree: true, attributes: true }
observer.observe(targetNode, config)

function checkLengths() {
    var len = arguments[0].length;
    for(var i = 1; i < arguments.length; i++) {
        if(arguments[i].length != len) {
            return false;
        }
    }
    return true;
}