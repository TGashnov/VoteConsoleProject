CREATE VIEW [dbo].[vwSearchVoteByAnswer]
	AS 
SELECT Question
	  ,STRING_AGG(Answer.[Text], ', ') AS Answers
FROM Vote
INNER JOIN Answer ON Vote.Id = VoteId
WHERE Vote.Id = (SELECT VoteId FROM Answer WHERE Answer.[Text] = 'Дарвин')
GROUP BY Question
GO
