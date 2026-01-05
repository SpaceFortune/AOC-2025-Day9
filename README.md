# AdventOfCodeDay9
• 	Verarbeitung einer Punktliste aus 
• 	Berechnung der maximalen Rechtecksfläche basierend auf gegebenen Koordinaten
Part 1
• 	Brute‑Force‑Vergleich aller Punktpaare
• 	Rechtecksfläche = 
• 	Nutzung von LINQ‑Query‑Syntax für die Paarbildung und Max‑Berechnung
Part 2
• 	Normalisierung der Koordinaten (Index‑Kompression)
• 	Aufbau eines Gitters aus Wänden ()
• 	Zeichnen eines geschlossenen Polygons aus den gegebenen Punkten
• 	Flood‑Fill / BFS vom äußeren Rand, um „Außenbereich“ zu markieren
• 	Bestimmung des Innenbereichs ()
• 	Prüfung, ob ein Rechteck vollständig innerhalb des erlaubten Bereichs liegt
• 	Ermittlung der maximalen gültigen Rechtecksfläche
Verwendete Techniken
• 	LINQ (, , , Query‑Syntax)
• 	2D‑Arrays für Grid‑Repräsentation
• 	Breadth‑First Search (BFS)
• 	Geometrische Rechteckprüfung
• 	Tuples für kompakte Punktdarstellung
• 	Index‑Kompression zur Reduktion der Grid‑Größe
Eingabe
• 	Datei  mit Zeilen im Format:

