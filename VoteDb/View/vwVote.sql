CREATE VIEW [dbo].[vwVote]
	AS 
SELECT Question
	  ,Note
	  ,STRING_AGG(Answer.[Text], ', ') AS Answers
	  ,STRING_AGG(Tag.[Text], ', ') AS Tags
	  ,(Vote.NumberOfVoters / (DATEDIFF(DAY, Created, GETDATE()))) AS VoteRating
	  ,VoteStatus.[Name] AS [Status]
FROM Vote
INNER JOIN Vote_Tag ON Vote.Id = Vote_Tag.Vote
INNER JOIN Tag ON Vote_Tag.Tag = Tag.Id
INNER JOIN Answer ON Vote.Id = VoteId
INNER JOIN VoteStatus ON Vote.[Status] = VoteStatus.Id
GROUP BY Question, Note, Vote.NumberOfVoters, Created, VoteStatus.[Name]
GO