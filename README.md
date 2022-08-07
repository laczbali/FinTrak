# FinTrak
Track spendings through charts and reports

# Requirements
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
- Database in AWS (Aurora Serverless)
- Code in .NET, running on AWS Lambda
- Receives POST requests, runs them through a ruleset
  - Automatically assign a category, based on source, amount

**Mobile**
- Reads SMS contents, sends it to the backend
