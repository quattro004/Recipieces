sudo service mongod start
mongo
use RecipeDb
db.createCollection('Category')
db.Category.createIndex( { "name": 1 }, { unique: true } )
db.createCollection('Recipe')
