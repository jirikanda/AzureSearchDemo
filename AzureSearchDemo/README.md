# Azure AI Search Demo

## Search

1. ClientProvider
2. CreateIndex (analyzer cs.lucene)
3. Populate data
4. QueryData
    * Sekce 1: Nenajde bez diakritity.
    * Sekce 2: Nenajde "stů".
5. AnalyzeText
6. CreateIndex (MyDemoAnalyzer1)    
7. QueryData
    * Sekce 1: Najde i s diakritikou (OK).
    * Sekce 2: Najde "stů". (OK).
    * Sekce 3: Rozdíl "jide* stu*" a "jide stu".
8. CreateIndex (MyDemoAnalyzer2)
    * Sekce 1-2: "OK" (v uvozovkách)
    * Sekce 4: "jedla soda" najde jídelní židle???
9. CreateIndex (MyDemoAnalyzer3)
    * Sekce 1-4: OK

## Facets
