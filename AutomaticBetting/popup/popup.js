chrome.storage.local.get(['elementsText', 'elementsUrl', 'elementsDataID'], (result) => {
  const elementsText = result.elementsText  
  const elementsUrl = result.elementsUrl
  const elementsDataID = result.elementsDataID

  console.log('elementsText: ' + result.elementsText.length)
  console.log('elementsUrl: ' + result.elementsUrl.length)
  console.log('elementsDataID: ' + result.elementsDataID.length)

  const list = document.getElementById('list')

  for (let i = 0; i < elementsText.length; i++) {
  //elementsUrl.forEach((text) => {
    const listItem = document.createElement('li')
    const item = document.createElement('a')
    item.textContent = elementsText[i]
    item.setAttribute('href', 'https://www.efortuna.pl' + elementsUrl[i])
    item.setAttribute('target', '_blank')
    item.dataset.id = elementsDataID[i]
    listItem.appendChild(item)
    list.appendChild(listItem)
  }
})