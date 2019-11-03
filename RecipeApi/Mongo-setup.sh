sudo service mongod start
mongo
use RecipeDb

db.createCollection('Category')
db.Category.insertOne({'Name':'Marinades, Rubs, Sauces','Description':'The best recipes for marinades, rubs and sauces.', 'CreatedOn': '2013-04-01T17:45:00', 'ModifiedOn': '2013-04-01T17:45:00'})
db.Category.insertOne({'Name':'Chicken','Description':'Meals containing chicken as the main ingredient.', 'CreatedOn': '2013-04-01T17:45:00', 'ModifiedOn': '2013-04-01T17:45:00'})
db.Category.insertOne({'Name':'Beef','Description':'Meals containing beef as the main ingredient.', 'CreatedOn': '2013-04-01T17:45:00', 'ModifiedOn': '2013-04-01T17:45:00'})
db.Category.insertOne({'Name':'Pork','Description':'Meals containing Pork as the main ingredient.', 'CreatedOn': '2013-04-01T17:45:00', 'ModifiedOn': '2013-04-01T17:45:00'})
db.Category.insertOne({'Name':'Turkey','Description':'Meals containing turkey as the main ingredient.', 'CreatedOn': '2013-04-01T17:45:00', 'ModifiedOn': '2013-04-01T17:45:00'})
db.Category.insertOne({'Name':'Salads','Description':'Nutritious and delicious!', 'CreatedOn': '2013-04-01T17:45:00', 'ModifiedOn': '2013-04-01T17:45:00'})
db.Category.insertOne({'Name':'Grilling','Description':'Grillin and chillin!', 'CreatedOn': '2013-04-01T17:45:00', 'ModifiedOn': '2013-04-01T17:45:00'})
db.Category.insertOne({'Name':'Drinks','Description':'Thirsty anyone?', 'CreatedOn': '2013-04-01T17:45:00', 'ModifiedOn': '2013-04-01T17:45:00'})
db.Category.insertOne({'Name':'Kids','Description':'For the picky tykes.', 'CreatedOn': '2013-04-01T17:45:00', 'ModifiedOn': '2013-04-01T17:45:00'})
db.Category.insertOne({'Name':'Casserole','Description':'Yee haw boys, yee haw boys', 'CreatedOn': '2013-04-01T17:45:00', 'ModifiedOn': '2013-04-01T17:45:00'})
db.Category.insertOne({'Name':'Sides','Description':'Gotta have em', 'CreatedOn': '2013-04-01T17:45:00', 'ModifiedOn': '2013-04-01T17:45:00'})

db.createCollection('Recipe')

show collections
db.Recipe.find()