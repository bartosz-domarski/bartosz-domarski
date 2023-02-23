function saveElement(selector) {
    const elementsText = []
    const elementsUrl = []
    const elementsDataID = []
    const elements = document.querySelectorAll(selector)

    elements.forEach(element => {
        elementsText.push(element.getElementsByClassName('ticket-bet-name').textContent),
        elementsUrl.push(element.querySelector('a[href]').getAttribute('href')),
        elementsDataID.push(element.dataset.id)
    })

    chrome.storage.local.set({ 'elementsText': elementsText, 'elementsUrl': elementsUrl,
        'elementsDataID': elementsDataID }, () => {
        console.log('Elements saved to local storage ' + elementsText.length)
    })
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