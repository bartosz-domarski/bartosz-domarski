chrome.storage.local.get(['availableSystems', 'elementsText', 'elementsUrl', 'elementsDataID', 'elementsDataValue'], (result) => {
  const availableSystems = result.availableSystems
  const elementsText = result.elementsText  
  const elementsUrl = result.elementsUrl
  const elementsDataID = result.elementsDataID
  const elementsDataValue = result.elementsDataValue
  const list = document.getElementById('list')

  function checkCouponsOdds(system) {
    let badCoupons = []
    let missingOdds = []

    for (let i = 0; i < system.length; i++) {
      let result = 1
      system[i].forEach((index) => {
        result *= elementsDataValue[index]
      })
      if (result < 100) {
        badCoupons.push(i+1)
        missingOdds.push((100/result).toFixed(2))
      }
    }

    if (badCoupons.length > 0) {
      alert('W kuponach nr: ' + badCoupons.join() + ' brakuje takich kursow: ' + missingOdds.join())
    } else {
      alert('Kursy sa ok!')
    }
  }

  function checkSystemOdds(system) {
    const system477 = [[1,0,0,1,0,1,1],[0,1,1,0,0,1,1],[0,1,0,1,1,0,1],[1,0,1,0,1,0,1],[0,0,1,1,1,1,0],[1,1,0,0,1,1,0],[1,1,1,1,0,0,0]]
    const system477Indexes = [[0,3,5,6],[1,2,5,6],[1,3,4,6],[0,2,4,6],[2,3,4,5],[0,1,4,5],[0,1,2,3]]

    const system4814 = [[0,0,0,0,1,1,1,1],[0,0,1,1,0,0,1,1],[1,1,0,0,0,0,1,1],[0,1,0,1,0,1,0,1],
      [1,0,1,0,0,1,0,1],[1,0,0,1,1,0,0,1],[0,1,1,0,1,0,0,1],[1,0,0,1,0,1,1,0],[0,1,1,0,0,1,1,0],
      [0,1,0,1,1,0,1,0],[1,0,1,0,1,0,1,0],[0,0,1,1,1,1,0,0],[1,1,0,0,1,1,0,0],[1,1,1,1,0,0,0,0]]
    const system4814Indexes = [[4,5,6,7],[2,3,6,7],[0,1,6,7],[1,3,5,7],[0,2,5,7],[0,3,4,7],[1,2,4,7],[0,3,5,6],[1,2,5,6],[1,3,4,6],
      [0,2,4,6],[2,3,4,5],[0,1,4,5],[0,1,2,3]]
    
    if (system.currentTarget.textContent == '4/7/7') {
      checkCouponsOdds(system477Indexes)
    } else if (system.currentTarget.textContent == '4/8/14') {
      checkCouponsOdds(system4814Indexes)
    } else {
      alert('System niedostępny')
    }
  }

  availableSystems.forEach(element => {
    const listSystems = document.createElement('div')

    const button = document.createElement('button')
    button.textContent = element
    button.setAttribute('type', 'button')
    button.addEventListener('click', checkSystemOdds)
    listSystems.appendChild(button)

    const input = document.createElement('input')
    input.setAttribute('type', 'number')
    input.setAttribute('min', '2')
    input.setAttribute('value', '0')
    listSystems.appendChild(input)

    list.appendChild(listSystems)
  });

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