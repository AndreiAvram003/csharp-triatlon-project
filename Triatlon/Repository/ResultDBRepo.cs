using System.Data;
using Triatlon.Domain;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Triatlon.Repository;

public class ResultDBRepo:ResultRepository
{
    
    private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    
    IDictionary<String, string> props;
    
    public ResultDBRepo(IDictionary<String, string> props)
    {
        log.Info("Creating ResultDBRepo ");
        this.props = props;
    }
    
    
    public Result Save(Result result)
        {
            log.Debug("Saving result: " + result);
            using (IDbConnection con = BDUtils.getConnection(props))
            {
                con.Open();
                try
                {
                    if (GetResultForParticipant(con, result.participant.id, result.trial.id) > 0)
                    {
                        log.Debug("Result already exists");
                        return null;
                    }

                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText =
                            "UPDATE results SET result = @Result WHERE participant_id = @ParticipantId AND trial_id = @TrialId";
                        
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@TrialId";
                        param.Value = result.trial.id;
                        
                        var param2 = cmd.CreateParameter();
                        param2.ParameterName = "@ParticipantId";
                        param2.Value = result.participant.id;
                        
                        var param3 = cmd.CreateParameter();
                        param3.ParameterName = "@Result";
                        param3.Value = result.result;

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            log.Debug("Result saved successfully: " + result);
                            UpdateParticipantPoints(con, result.participant.id, result.result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error while saving result: " + ex.Message);
                }
            }

            return result;
        }

        private int GetResultForParticipant(IDbConnection con, long participantId, long trialId)
        {
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT result FROM results WHERE participant_id = @ParticipantId AND trial_id = @TrialId";
                var param = cmd.CreateParameter();
                param.Value = participantId;
                param.ParameterName = "@ParticipantId";
                
                var param2 = cmd.CreateParameter();
                param2.Value = trialId;
                param2.ParameterName = "@TrialId";
                

                object result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }

        private void UpdateParticipantPoints(IDbConnection con, long participantId, int pointsToAdd)
        {
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "UPDATE participants SET points = points + @PointsToAdd WHERE id = @ParticipantId";
                
                var param = cmd.CreateParameter();
                param.Value = pointsToAdd;
                param.ParameterName = "@PointsToAdd";
                
                var param2 = cmd.CreateParameter();
                param2.Value = participantId;
                param2.ParameterName = "@ParticipantId";

                cmd.ExecuteNonQuery();
            }
        }

    public Result GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Result Update(Result obj)
    {
        throw new NotImplementedException();
    }

    public Result DeleteById(long id)
    {
        throw new NotImplementedException();
    }

    public List<Result> GetAll()
    {
        throw new NotImplementedException();
    }
}