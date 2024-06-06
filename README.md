API "MyShop"

L'api doit nous permettre de gérer des offres qui doivent avoir les données suivantes :

{

  "ProductId": 42,
  
  "ProductName": "T-Shirt",
  
  "ProductBrand": "Olympp",
  
  "ProductSize": "M",
  
  "Quantity": 100,
  
  "Price": 42.42
  
}

routes :

/api/offer/all -> Retourne la liste des offres

/api/offer/add -> Ajoute une nouvelle offre

/api/offer/update -> Modifie une offre existante

Data

Le stockage des données doit être découpé de la façon suivante:

dbo.product : doit contenir un identifiant, un nom, une marque et une taille.

dbo.price : doit contenir un prix par produit.

dbo.stock : doit contenir une quantité par produit.

L'idée serait d'avoir une centaine de lignes dans ces tables lors du démarrage de 
l'application.
