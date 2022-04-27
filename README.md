# Tri-Share
Usage


Steps
1. Firstly download and rebuild packages npm install. (Front End)
2. Logger is a class library using NLog. This is loaded as a dll, and won't open
3. Select Trifork API (MVC .NET CORE)
4. Then Start, using Swashbuckle testing should be available


Testing:
1. *A transaction cannot be made until a group has first been created.*
2. creating the group completing the first and last name of each added particiant, ensuring they are unique (Ideally the system should handle a situattion when these names are not unique within the given group. However, here the id is based on the first and last names);
3. Once the group has been been created, copy the returned Guid, then use this Id in the transaction area as the 'GroupId' then add each transaction.
4. Once you have created transactions, you may generate a ledger based on the groupId.
