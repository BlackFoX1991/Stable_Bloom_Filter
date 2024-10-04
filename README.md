# stable Bloom Filter

In diesem Beispiel die weiterentwickelte Variante der stable Bloom Filter der nicht wie das Original auf Basis von einem Bit-Array arbeitet sondern dazu ein Integer Array nutzt und so die Möglichkeit bietet jeden slot bis zu einer maximalen Anzahl zu verwenden. Dazu kommt ein sogenannter verfall Mechanismus der auf Basis eines verfall Faktors Elemente nach und nach "verblassen" lässt. Auf Basis der eingefügten Elemente werden wird dieser Faktor je nach Belegungsgrenze des Filters dynamisch justiert. Der stable Bloom Filter frisst mehr Ressourcen ist jedoch genauso effizient wie der standard Bloom Filter und bietet im gegensatz zum original eine stabilere FPR ( FalsePositiveRate ). Zusätzlich kann die FPR durch das abändern der Hash-Funktionen / Anzahl der verwendeten Hash-Funktionen und die größe des Filters beeinflusst werden.

Ein Beispiel dafür wo Bloom Filter eingesetzt werden sind zum Beispiel NoSQL Datenbank Systeme. Um unnötige I/O Zugriffe zu vermeiden wird ein sogenannter Lookup gestartet und erst einmal geschaut ob
z.b. ein Schlüssel oder Index eventuell vorhanden sein könnte. Ein Prozess bei dem eine gewisse FPR kein Problem ist. So kann ein Teil der I/O Zugriffe zumindest verringert werden was Ressourcen spart und die Effizienz erhöht. Ebenso ist der Bloom Filter bei Peer to Peer Verbindungen nützlich. So kann geschaut werden ob ein Teil der Daten bereits vorhanden ist, so werden weniger Ressourcen gebraucht.


Infos zum Bloom Filter
https://en.wikipedia.org/wiki/Bloom_filter
