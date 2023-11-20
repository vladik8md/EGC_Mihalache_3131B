
# Laborator 3

---

**1.** Care este ordinea de desenare a vertexurilor pentru aceste metode (orar sau anti-orar)?

Ordinea de desenare a vertexurilor pentru triunghiul specificat este în sens orar. Aceasta înseamnă că vertexurile sunt specificate în ordinea în care acoperă zona triunghiului în sensul acelor de ceasornic. În acest caz, ordinea vertexurilor este următoarea:

- Vertex cu coordonatele (-1.0f, 1.0f) (stânga sus)

- Vertex cu coordonatele (0.0f, -1.0f) (centru jos)

- Vertex cu coordonatele (1.0f, 1.0f) (dreapta sus)

---

**2.** Ce este anti-aliasing? Prezentați această tehnică pe scurt.

Anti-aliasing este o tehnică folosită în grafică computerizată pentru a reduce sau elimina efectul de zimțare (aliasing) care apare atunci când se desenează linii sau obiecte pe un ecran cu rezoluție limitată.

Tehnica anti-aliasing funcționează prin adăugarea unor pixeli suplimentari sau prin atenuarea culorilor în zonele de tranziție între obiectele desenate și fundalul din jurul lor. Acest lucru face ca marginile obiectelor să pară mai netede și mai naturale, eliminând aspectul denticios sau treptat al liniilor sau marginilor.

---

**3.** Care este efectul rulării comenzii **GL.LineWidth(float)**? Dar pentru
**GL.PointSize(float)**? Funcționează în interiorul unei zone
**GL.Begin()**?

Comanda **GL.LineWidth(float)** setează lățimea liniei pentru viitoarele desene de linii realizate cu OpenGL, iar **GL.PointSize(float)** setează dimensiunea punctelor pentru viitoarele desene de puncte.

Aceste comenzi funcționează pentru viitoarele desene realizate în interiorul unei secțiuni **GL.Begin()** și **GL.End()**. Deci, aceste setări pentru lățimea liniei și dimensiunea punctelor vor afecta doar obiectele desenate între apelurile **GL.Begin()** și **GL.End()**.

---

**4.a.** Care este efectul utilizării directivei **LineLoop** atunci când sunt
desenate segmente de dreaptă multiple în OpenGL?

Efectul utilizării directivei **GL.LineLoop** este acela că ea creează un contur închis format din segmente de dreaptă care conectează punctele în ordine. 

---

**4.b.** Care este efectul utilizării directivei **LineStrip** atunci când sunt
desenate segmente de dreaptă multiple în OpenGL?

Efectul utilizării directivei **GL.LineStrip** este acela că ea creează un traseu continuu format din segmente de dreaptă care se conectează unul după altul în ordine.

---

**4.c.** Care este efectul utilizării directivei **TriangleFan** atunci când sunt
desenate segmente de dreaptă multiple în OpenGL?

Efectul utilizării directivei **GL.TriangleFan** este acela că fiecare punct specificat în interiorul secțiunii **GL.Begin()** și **GL.End()** devine un vârf al unui triunghi, iar punctul central al fanului de triunghiuri este vârful central de origine.

---

**4.d.** Care este efectul utilizării directivei **TriangleStrip** atunci când sunt
desenate segmente de dreaptă multiple în OpenGL?

Efectul utilizării directivei **GL.TriangleStrip** este acela că ea creează un șir continuu de triunghiuri conectate, în care fiecare triunghi împarte două vârfuri cu triunghiul anterior.

---

**5.** De ce este importantă utilizarea de culori diferite (în gradient sau
culori selectate per suprafață) în desenarea obiectelor 3D? Care este
avantajul?

Utilizarea de culori diferite în desenarea obiectelor 3D în OpenGL are câteva avantaje importante:

- Reprezentare vizuală clară
- Îmbunătățirea detaliilor și adâncimii
- Realism și estetică

---

**6.** Ce reprezintă un gradient de culoare? Cum se obține acesta în
OpenGL?

Un gradient de culoare reprezintă o tranziție lină între două sau mai multe culori, în care culorile se amestecă treptat pentru a crea o trecere uniformă între ele.

În OpenGL, un gradient de culoare poate fi obținut folosind metoda numită "Interpolare de culoare". Aceasta implică specificarea a două sau mai multe culori, precum și punctele în care aceste culori trebuie să apară într-un triunghi, dreptunghi sau oricare altă formă geomerică. OpenGL va interpola (a combina) culorile în mod automat între aceste puncte pentru fiecare fragment (pixel) din formă, obținând astfel un gradient de culoare uniform.

---

**7.** Ce reprezintă un gradient de culoare? Cum se obține acesta în
OpenGL?

Un gradient de culoare reprezintă o tranziție lină între două sau mai multe culori, în care culorile se amestecă treptat pentru a crea o trecere uniformă între ele.

În OpenGL, un gradient de culoare poate fi obținut folosind metoda numită "Interpolare de culoare". Aceasta implică specificarea a două sau mai multe culori, precum și punctele în care aceste culori trebuie să apară într-un triunghi, dreptunghi sau oricare altă formă geomerică. OpenGL va interpola (a combina) culorile în mod automat între aceste puncte pentru fiecare fragment (pixel) din formă, obținând astfel un gradient de culoare uniform.

---

**8.** Ce efect va apare la utilizarea canalului de transparență?

Utilizarea canalului de transparență va afecta gradul de opacitate sau vizibilitate a obiectelor sau texturilor desenate, permițându-le să fie mai sau mai puțin transparente, în funcție de valoarea acestui canal. Astfel, acest canal determină cât de transparent sau opac va fi obiectul, iar efectul va fi o schimbare în vizibilitate sau în gradul de transparență al elementelor grafice.

---

**9.** Ce efect are utilizarea unei culori diferite pentru fiecare vertex
atunci când desenați o linie sau un triunghi în modul strip?

Utilizarea culorilor diferite pentru fiecare vârf în modul strip vă oferă controlul asupra modului în care culoarea se comportă pe întreaga lungime a liniei sau marginea triunghiului.

---