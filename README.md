# FinTrak
Track spendings through charts and reports

# TODO
1. ~~HelloWorld in Lambda~~
2. Setup autodeploy from master branch
3. HelloWorld with database
   1. Basic setup
   2. Schema
   3. Access

# Feature goals
**Frontend**
- Displays spendings
  - Every item, in a list
  - Categorized (custom-defined categories)
  - Monthly breakdown
  - Averages by month/quarter/year, by category
 - Displays graphs
 - Create custom reports?
  - Write SQL in browser
  - Optinally configure a graph output (which column should be the X label, which should be series 1/2/etc)
 - Upload spendings by hand
 - Edit already uploaded spendings (highlights uncategorized spendings)
 - Define rules for handling incoming spending POSTs
 - Define RegEx for handling incoming POSTs

**Backend**
- Database in AWS (Aurora Serverless, [or DynamoDB?](https://aws.amazon.com/free/database/))
- Code in .NET, running on AWS Lambda
- Receives POST requests, runs them through a ruleset
  - Automatically assign a category, based on source, amount

**Mobile**
- Reads SMS contents, sends it to the backend
