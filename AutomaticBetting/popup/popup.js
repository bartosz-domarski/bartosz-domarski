chrome.storage.local.get(['availableSystems', 'elementsText', 'elementsUrl', 'elementsDataID', 'elementsDataValue'], (result) => {
  const availableSystems = result.availableSystems
  const elementsText = result.elementsText  
  const elementsUrl = result.elementsUrl
  const elementsDataID = result.elementsDataID
  const elementsDataValue = result.elementsDataValue
  const list = document.getElementById('list')

  function checkSystemOdds(system) {
    const system477 = [[1,0,0,1,0,1,1],[0,1,1,0,0,1,1],[0,1,0,1,1,0,1],[1,0,1,0,1,0,1],[0,0,1,1,1,1,0],[1,1,0,0,1,1,0],[1,1,1,1,0,0,0]]
    const system477Indexes = [[0,3,5,6],[1,2,5,6],[1,3,4,6],[0,2,4,6],[2,3,4,5],[0,1,4,5],[0,1,2,3]]
    let badCoupons = []

    if (system.currentTarget.textContent == '4/7/7') {
        for (let i = 0; i < system477Indexes.length; i++) {
          let result = 1
          system477Indexes[i].forEach((index) => {
            result *= elementsDataValue[index]
          })
          if (result < 100) {
            let ix = i + 1
            let missingOdds = (100/result).toFixed(2)
            badCoupons.push({key: ix, value: missingOdds})
          }
        }

        if (badCoupons.length > 0) {
          alert('W kuponach nr: ' + Object.keys(badCoupons).join() + ' brakuje takich kursow: ' + Object.values(badCoupons).join())
        } else {
          alert('Kursy sa ok!')
        }
    } else {
      alert('System niedostępny')
    }
  }

  const listSystems = document.createElement('div')
  availableSystems.forEach(element => {
    const item = document.createElement('button')
    item.textContent = element
    item.setAttribute('type', 'button')
    item.addEventListener('click', checkSystemOdds)
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