import java.util.Properties;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

import java.io.FileInputStream;

/**
 * Runs queries against a back-end database
 */
public class Query {
	private static Properties configProps = new Properties();

	private static String MySqlServerDriver;
	private static String MySqlServerUrl;
    private static String MySqlServerUser;
	private static String MySqlServerPassword;
	
	private static String PsotgreSqlServerDriver;
	private static String PostgreSqlServerUrl;
	private static String PostgreSqlServerUser;
	private static String PostgreSqlServerPassword;


	// DB Connection
	private Connection _mySqlDB; //IMDB
    private Connection _postgreSqlDB; //customer_DB

	// Canned queries

	private String _search_sql = "SELECT * FROM movie_info WHERE movie_name like ? ORDER BY movie_id";
	private PreparedStatement _search_statement;

	private String _producer_id_sql = "SELECT y.* "
					 + "FROM producer_movies x, producer_ids y "
					 + "WHERE x.movie_id = ? and x.producer_id = y.producer_id";
	private PreparedStatement _producer_id_statement;

	private String _customer_login_sql = "SELECT * FROM customer WHERE login = ? and password = ?";
	private PreparedStatement _customer_login_statement;

	private String _begin_transaction_read_write_sql = "START TRANSACTION";
	private PreparedStatement _begin_transaction_read_write_statement;

	private String _commit_transaction_sql = "COMMIT";
	private PreparedStatement _commit_transaction_statement;

	private String _rollback_transaction_sql = "ROLLBACK";
	private PreparedStatement _rollback_transaction_statement;

    private String _check_rent = "SELECT max(times_rented) from rental WHERE cid = ? and movie_id = ?";
    private String _insert_rent = "INSERT INTO rental (cid, movie_id, status,times_rented)"
        +" VALUES (?,?,'open', 1)";
    private PreparedStatement _new_rent_statement;

    private String _update_rent = "UPDATE ("+_check_rent+")"
                                    +" SET status = ?,"
                                    +" times_rented = ?";
    private PreparedStatement _old_rent_statement;


    private String _max_rentals_query = "SELECT max_rentals FROM plan as p "
            + "inner join customer as c ON c.plan = p.plan_id";
    private PreparedStatement _max_rentals_statement;
	public Query() {
	}

    /**********************************************************/
    /* Connection to MySQL database */

	public void openConnections() throws Exception {
        
        /* open connections to TWO databases: movie and  customer databases */
        
		configProps.load(new FileInputStream("dbconn.config"));
        
		MySqlServerDriver    = configProps.getProperty("MySqlServerDriver");
		MySqlServerUrl 	   = configProps.getProperty("MySqlServerUrl");
		MySqlServerUser 	   = configProps.getProperty("MySqlServerUser");
		MySqlServerPassword  = configProps.getProperty("MySqlServerPassword");
        
        PsotgreSqlServerDriver    = configProps.getProperty("PostgreSqlServerDriver");
        PostgreSqlServerUrl 	   = configProps.getProperty("PostgreSqlServerUrl");
        PostgreSqlServerUser 	   = configProps.getProperty("PostgreSqlServerUser");
        PostgreSqlServerPassword  = configProps.getProperty("PostgreSqlServerPassword");
                              
		/* load jdbc driver for MySQL */
		Class.forName(MySqlServerDriver);

		/* open a connection to your mySQL database that contains the movie database */
		_mySqlDB = DriverManager.getConnection(MySqlServerUrl, // database
				MySqlServerUser, // user
				MySqlServerPassword); // password
		
     
        /* load jdbc driver for PostgreSQL */
        Class.forName(PsotgreSqlServerDriver);
        
         /* connection string for PostgreSQL */
        String PostgreSqlConnectionString = PostgreSqlServerUrl+"?ssl=true&sslfactory=org.postgresql.ssl.NonValidatingFactory&user="+
        		PostgreSqlServerUser+"&password=" + PostgreSqlServerPassword;
        
        
        /* open a connection to your postgreSQL database that contains the customer database */
        _postgreSqlDB = DriverManager.getConnection(PostgreSqlConnectionString);
        
	
	}

	public void closeConnections() throws Exception {
		_mySqlDB.close();
        _postgreSqlDB.close();
	}

    /**********************************************************/
    /* prepare all the SQL statements in this method.
      "preparing" a statement is almost like compiling it.  Note
       that the parameters (with ?) are still not filled in */

	public void prepareStatements() throws Exception {

		_search_statement = _mySqlDB.prepareStatement(_search_sql);
		_producer_id_statement = _mySqlDB.prepareStatement(_producer_id_sql);

		/* uncomment after you create your customers database */
		_customer_login_statement = _postgreSqlDB.prepareStatement(_customer_login_sql);
		_begin_transaction_read_write_statement = _postgreSqlDB.prepareStatement(_begin_transaction_read_write_sql);
		_commit_transaction_statement = _postgreSqlDB.prepareStatement(_commit_transaction_sql);
		_rollback_transaction_statement = _postgreSqlDB.prepareStatement(_rollback_transaction_sql);

		/* add here more prepare statements for all the other queries you need */
		/* . . . . . . */
        _new_rent_statement = _postgreSqlDB.prepareStatement(_insert_rent);
        _old_rent_statement = _postgreSqlDB.prepareStatement(_update_rent);
	}


    /**********************************************************/
    /* suggested helper functions  */

	public int helper_compute_remaining_rentals(int cid) throws Exception {
		/* how many movies can she/he still rent ? */
        ResultSet max_rentals = q.executeQuery(max_rentals_query);
        
        String outstanding_query = "SELECT count(*) from rental where cid = "+cid+ " and status = 'open'"
        ResultSet outstanding = q.executeQuery(outstanding_query);
		/* you have to compute and return the difference between the customer's plan
		   and the count of outstanding rentals */
		return (maxrentals.getInt() - );
	}

	public String helper_compute_customer_name(int cid) throws Exception {
		/* you find  the name of the current customer */
        String q = "SELECT first_name || ' ' || last name FROM customer WHERE cid= "+cid + ";";
        // call the query 
        ResultSet name = q.executeQuery();
		return (name.getString());

	}

	public boolean helper_check_plan(int plan_id) throws Exception {
		/* is plan_id a valid plan id?  you have to figure out */
        String q = " SELECT plan_id"
		return true;

	}

	public boolean helper_check_movie(String movie_id) throws Exception {
		/* is movie_id a valid movie id? you have to figure out  */
		return true;
	}

	private int helper_who_has_this_movie(String movie_id) throws Exception {
		/* find the customer id (cid) of whoever currently rents the movie movie_id; return -1 if none */
		return (77);
	}

    /**********************************************************/
    /* login transaction: invoked only once, when the app is started  */
	public int transaction_login(String name, String password) throws Exception {
		/* authenticates the user, and returns the user id, or -1 if authentication fails */

		int cid;

		_customer_login_statement.clearParameters();
		_customer_login_statement.setString(1,name);
		_customer_login_statement.setString(2,password);
	    ResultSet cid_set = _customer_login_statement.executeQuery();
	    if (cid_set.next()) cid = cid_set.getInt(1);
		else cid = -1;
		return(cid);
		return (55); //comment after you create your own customers database
	}

	public void transaction_personal_data(int cid) throws Exception {
		/* println the customer's personal data: name and number of remaining rentals */
	}


    /**********************************************************/
    /* main functions in this project: */

	public void transaction_search(int cid, String movie_name)
			throws Exception {
		/* searches for movies with matching names: SELECT * FROM movie WHERE movie_name LIKE name */
		/* prints the movies, producers, actors, and the availability status:
		   AVAILABLE, or UNAVAILABLE, or YOU CURRENTLY RENT IT */

		/* set the first (and single) '?' parameter */
		_search_statement.clearParameters();
		_search_statement.setString(1, '%' + movie_name + '%');

		ResultSet movie_set = _search_statement.executeQuery();
		while (movie_set.next()) {
			String movie_id = movie_set.getString(1);
			System.out.println("ID: " + movie_id + " NAME: "
					+ movie_set.getString(2) + " YEAR: "
					+ movie_set.getString(3) + " RATING: "
					+ movie_set.getString(4));
			/* do a dependent join with producer */
			_producer_id_statement.clearParameters();
			_producer_id_statement.setString(1, movie_id);
			ResultSet producer_set = _producer_id_statement.executeQuery();
			while (producer_set.next()) {
				System.out.println("\t\tProducer name: " + producer_set.getString(2));
			}
			producer_set.close();
			/* now you need to retrieve the actors, in the same manner */
			/* then you have to find the status: of "AVAILABLE" "YOU HAVE IT", "UNAVAILABLE" */
		}
		System.out.println();
	}

	public void transaction_choose_plan(int cid, int pid) throws Exception {
	    /* updates the customer's plan to pid: UPDATE customer SET plid = pid */
	    /* remember to enforce consistency ! */
	}

	public void transaction_list_plans() throws Exception {
	    /* println all available plans: SELECT * FROM plan */
	}

	public void transaction_rent(int cid, String movie_id) throws Exception {
	    /* rend the movie movie_id to the customer cid */
	    /* remember to enforce consistency ! */
        _check_rent.clearParameters();
        _check_rent.setString(1,'%' + cid + '%',2, '%' + movie_id + '%' );

        ResultSet check_existing = _check_rent.executeQuery();
        int rented = 0;
        String open = "open";
        if (check_existing.first()) //not empty
        {
            _old_rent_statement.clearParameters();
            _old_rent_statement.setString(1,'%' + cid + '%',2, '%' + movie_id + '%' , 3, '%'+open+'%',rented++ );

        }else  //is empty
        {
            _new_rent_statement.clearParameters();
            _new_rent_statement.setString(1,'%' + cid + '%',2, '%' + movie_id + '%' );
        }

	}

	public void transaction_return(int cid, String movie_id) throws Exception {
	    /* return the movie_id by the customer cid */
	}

	public void transaction_fast_search(int cid, String movie_name)
			throws Exception {
		/* like transaction_search, but uses joins instead of dependent joins
		   Needs to run three SQL queries: (a) movies, (b) movies join producers, (c) movies join actors
		   Answers are sorted by movie_id.
		   Then merge-joins the three answer sets */
	}

}
