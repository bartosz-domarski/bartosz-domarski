chrome.storage.local.get(['availableSystems', 'elementsText', 'elementsUrl', 'elementsDataID', 'elementsDataValue'], (result) => {
  const availableSystems = result.availableSystems
  const elementsText = result.elementsText  
  const elementsUrl = result.elementsUrl
  const elementsDataID = result.elementsDataID
  const elementsDataValue = result.elementsDataValue
  const list = document.getElementById('list')

  console.log('availableSystems: ' + result.availableSystems.length)
  console.log('elementsText: ' + result.elementsText.length)
  console.log('elementsUrl: ' + result.elementsUrl.length)
  console.log('elementsDataID: ' + result.elementsDataID.length)
  console.log('elementsDataValue: ' + result.elementsDataValue.length)

  function systems(system) {
    console.log(system.currentTarget.textContent)
  }

  const listSystems = document.createElement('div')
  availableSystems.forEach(element => {
    const item = document.createElement('button')
    item.textContent = element
    item.setAttribute('type', 'button')
    item.addEventListener('click', systems)
    listSystems.appendChild(item)
  });
  list.appendChild(listSystems)

  for (let i = 0; i < elementsText.length; i++) {
    const listElements = document.createElement('li')
    const item = document.createElement('a')

    item.textContent = elementsText[i]
    item.setAttribute('href', 'https://www.efortuna.pl' + elementsUrl[i])
    item.setAttribute('target', '_blank')
    item.dataset.id = elementsDataID[i]
    item.dataset.value = elementsDataValue[i]
    
    listElements.appendChild(item)
    list.appendChild(listElements)
  }
})