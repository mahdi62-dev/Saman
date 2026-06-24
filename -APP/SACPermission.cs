namespace Saman.APP.SAC;

public class SACPermission
{
    public bool CheckPermission(int ModID, int ActionID)
    {
        var hasPermission = false;
        var rowCount = GL.dtSACUserPermission.Select("ModID=" + ModID + " AND ActionID=" + ActionID).Length;

        if (rowCount > 0) hasPermission = true;

        return hasPermission;
    }
}