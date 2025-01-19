bubble

- soap bubble
  - usually last for only a few seconds before bursting, either on their own or on contact with another object
  - floating / bursting: platform
  - color: the colors seen in a soap bubble arise from light [wave interference](https://en.wikipedia.org/wiki/Wave_interference) 
  - size
- bubble sort



core gameplay: bubble size

- bubble gets smaller as time goes by
  - pros: floating slower, more score, longer life
  - cons: move slower, more probably to be contacted
- bubble gets bigger when contact other bubbles in the level
  - pros: flexibility (move faster, less probably contacted)
  - cons: floating faster, shorter life



3c

- [x] player
  - [x] [A] a bubble 
  - [x] [A] show life time
- [ ] camera:
  - [x] [A] follow player
  - [ ] [B] camera impulse
- [ ] control
  - [x] [A] keyboard (move only horizontally)
  - [ ] gamepad



system

- [x] ui
  - [x] [A] main menu, play button and exit button
  - [x] [A] pause menu, resume button and exit button
  - [x] hud
- [x] game loop
  - [x] scores ?
  - [x] game over
- [ ] packaging test



level

- [x] bubble spawner
- [x] something contact with bubbles



others

- [x] **design** 
  - [x] [A] level design
    - [x] fixed area
    - [x] shorter life
- [ ] **art** 
  - [x] [A] bursting effect
  - [x] [A] merging effect
  - [ ] [A] main menu art
  - [ ] [A] bubble(player) art
  - [ ] [B] level art
  - [ ] [B] audio