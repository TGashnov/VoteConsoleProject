CREATE VIEW [dbo].[vwVoteAndTag]
	AS 
SELECT Question, STRING_AGG(Tag.[Text], ', ') AS Tags
FROM Vote
INNER JOIN Vote_Tag ON Vote.Id = Vote_Tag.Vote
INNER JOIN Tag ON Vote_Tag.Tag = Tag.Id
GROUP BY Question
GO

