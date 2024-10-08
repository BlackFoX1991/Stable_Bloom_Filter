# stable Bloom Filter

In diesem Beispiel die weiterentwickelte Variante der stable Bloom Filter der nicht wie das Original mit nur einem einfachen BitArray arbeitet sondern mit einem erweiterten das pro slot mehrere Plätze bietet die gesetzt werden können und so die Möglichkeit bietet jeden slot bis zu dieser selbst festgelegten maximalen Anzahl weiterer Bits zu verwenden. Dazu kommt ein sogenannter verfall Mechanismus der auf Basis eines verfall Faktors Elemente nach und nach "verblassen" lässt. Auf Basis der eingefügten Elemente werden wird dieser Faktor je nach Belegungsgrenze des Filters dynamisch justiert. Der stable Bloom Filter frisst mehr Ressourcen ist jedoch genauso effizient wie der standard Bloom Filter und bietet im gegensatz zum original eine stabilere FPR ( FalsePositiveRate ). Zusätzlich kann die FPR durch das abändern der Hash-Funktionen / Anzahl der verwendeten Hash-Funktionen und die größe des Filters beeinflusst werden.

Ein Beispiel dafür wo Bloom Filter eingesetzt werden sind zum Beispiel NoSQL Datenbank Systeme. Um unnötige I/O Zugriffe zu vermeiden wird ein sogenannter Lookup gestartet und erst einmal geschaut ob
z.b. ein Schlüssel oder Index eventuell vorhanden sein könnte. Ein Prozess bei dem eine gewisse FPR kein Problem ist. So kann ein Teil der I/O Zugriffe zumindest verringert werden was Ressourcen spart und die Effizienz erhöht. Ebenso ist der Bloom Filter bei Peer to Peer Verbindungen nützlich. So kann geschaut werden ob ein Teil der Daten bereits vorhanden ist, so werden weniger Ressourcen gebraucht.


Infos zum Bloom Filter
https://en.wikipedia.org/wiki/Bloom_filter


__Update vom 06.10.2024__
Ich habe den Code etwas nachbearbeitet, nun wird kein Integer-Array mehr verwendet sondern ein Bit-Array in einem weiteren BitArray. Ich nenne es mal das erweiterte Bit-Array.
So hat jedes BitArray mehrere slots die wie oben bereits beschrieben mehrere Plätze die gesetzt werden können. Das Ganze dient einfach zur Demonstration wie die erweiterte Variante
des Bloom Filters arbeitet.
