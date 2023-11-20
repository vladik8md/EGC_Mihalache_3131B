
# Laborator 2

---

**1.** Modificați valoarea constantei „MatrixMode.Projection”. Ce
observați?

**GL.Ortho(left, right, bottom, top, zNear, zFar);** // Configurarea proiecției ortografice

În acest exemplu, left, right, bottom, top, zNear și zFar sunt parametri care controlează modul în care obiectele sunt proiectate pe ecran. Modificând acești parametri în funcția GL.Ortho(), putem controla cum vor fi afișate obiectele în proiecția ortografică.

---

**2.** Ce este un viewport?

Un viewport reprezintă o regiune dreptunghiulară pe o suprafață de desen în care se desenează sau se randează o scenă sau o imagine.

---

**3.** Ce reprezintă conceptul de frames per seconds din punctul de
vedere al bibliotecii OpenGL?

În contextul bibliotecii OpenGL, conceptul de frames per second (FPS) se referă la numărul de cadre de imagine (frames) care sunt randate și afișate pe ecran într-o secundă de către o aplicație OpenGL.

---

**3.** Când este rulată metoda OnUpdateFrame()?

Metoda OnUpdateFrame() este rulată în fiecare cadru (frame) de afișare într-o aplicație OpenGL care utilizează biblioteca OpenTK pentru gestionarea graficii.

OnUpdateFrame() este apelată în mod repetat pentru a actualiza logica jocului sau pentru a face orice alte actualizări necesare înainte de a afișa următorul cadru pe ecran.

---

**4.** Ce este modul imediat de randare?

Modulul imediat (Immediate Mode) de randare este o abordare de randare grafică care a fost folosită în OpenGL până la versiunea 3.0, când a fost înlocuită de un model de randare mai modern și mai eficient, numit modul trăsături (Feature-based Mode) sau modul bazat pe șeidere. 

În modulul imediat de randare, desenarea obiectelor 3D se face prin intermediul unei serii de apeluri de funcții OpenGL pentru a specifica vertexele, culorile, texturile și alte proprietăți ale obiectelor. Acest mod este adesea denumit și "modul de desenare imediată" sau "modul fix de funcționare".

---

**5.** Care este ultima versiune de OpenGL care acceptă modul imediat?

Ultima versiune a OpenGL care acceptă oficial modul imediat de randare este OpenGL 2.1.

---

**6.** Când este rulată metoda OnRenderFrame()?

Metoda OnRenderFrame() dintr-o aplicație OpenTK este rulată pentru fiecare cadru de afișare și este folosită pentru a desena scenele 3D pe ecran. Această metodă este apelată în mod continuu, de obicei la o rată specificată, cum ar fi numărul de cadre pe secundă specificat în apelul Run() al aplicației.

---

**7.** De ce este nevoie ca metoda OnResize() să fie executată cel puțin
o dată?

Metoda OnResize() dintr-o aplicație OpenGL trebuie să fie executată cel puțin o dată pentru a inițializa corect viewport-ul și matricea de proiecție a scenelor 3D, astfel încât să corespundă dimensiunilor ferestrei de afișare.

---

**8.** Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia?

Parametrii metodei CreatePerspectiveFieldOfView() sunt:

- fieldOfView (float)
  - Domeniul de valori tipic pentru acest parametru este între 0 și 180 de grade, cu valori de aproximativ 60-90 de grade fiind comune pentru aplicații 3D.
- aspectRatio (float)
  - De obicei, aspect ratio-ul este setat la width / height, unde width și height reprezintă dimensiunile ferestrei.
- zNear (float)
  - Valoarea zNear trebuie să fie pozitivă și nu trebuie să fie prea mică pentru a evita artefacte de renderizare.
- zFar (float)
  - Valoarea zFar trebuie să fie pozitivă și mai mare decât zNear.


---