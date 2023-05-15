--
-- Table structure for table sw_audit_trail
--
CREATE TABLE sw_audit_trail (
	profile_id       varchar(1000) NULL,
	audit_date       timestamp NULL,
	"event"          varchar(1000) NULL,
	action_performed varchar(1000) NULL,
	entity           varchar(200) NULL
);
CREATE INDEX sw_audit_trail_profile_id_idx ON public.sw_audit_trail USING btree (profile_id, audit_date, event);

--
-- Table structure for table sw_country_currency
--
CREATE TABLE sw_country_currency (
	country_id             varchar(15) NOT NULL,
	country_name           varchar(50) NOT NULL,
	country_code           varchar(5) NOT NULL,
	currency_code          varchar(5) NOT NULL,
	flg_status             bpchar(1) NOT NULL,
	CONSTRAINT sw_country_currency_pk PRIMARY KEY (country_id)
);
CREATE INDEX sw_country_currency_country_id_idx ON sw_country_currency USING btree (country_id);

--
-- Table structure for table sw_exception_log
--
CREATE TABLE sw_exception_log (
	profile_id         varchar(1000) NULL,
	entity             varchar(30) NULL,
	error_log_date     date NULL,
	error_message      varchar(1000) NULL,
	error_param        varchar(1000) NULL
);
CREATE INDEX sw_exception_log_profile_id_idx ON sw_exception_log USING btree (profile_id, error_log_date);

--
-- Table structure for table sw_fees
--
CREATE TABLE sw_fees (
	fees_id                varchar(15) NOT NULL,
	fee_name               varchar(50) NOT NULL,
	fee_type               varchar(20) NOT NULL,
	fee_convenience        float8 NOT NULL,
	fee_transaction        float8 NOT NULL,
	flg_status             bpchar(1) NOT NULL,
	CONSTRAINT sw_fees_pk  PRIMARY KEY (fees_id)
);
CREATE INDEX sw_fees_fees_id_idx ON sw_fees USING btree (fees_id);

--
-- Table structure for table sw_sequence_mast
--
CREATE TABLE sw_sequence_mast (
	seq_name         varchar(40) NOT NULL,
	seq_prefix       varchar(20) NULL,
	seq_length       numeric NULL DEFAULT 0,
	seq_value        numeric NULL DEFAULT 0,
	seq_status       bpchar(1) NULL DEFAULT 'A'::bpchar,
	seq_date         varchar(20) NULL,
	CONSTRAINT sw_sequence_mast_pk PRIMARY KEY (seq_name)
);
CREATE INDEX sw_sequence_mast_seq_name_idx ON sw_sequence_mast USING btree (seq_name);

--
-- Table structure for table sw_transaction_log
--
CREATE TABLE sw_transaction_log (
	txn_date         timestamp(6) NOT NULL,
	txn_id           varchar(15) NOT NULL,
	profile_id       varchar(1000) NOT NULL,
	reference_id     varchar(1000) NOT NULL,
    txn_type         varchar(50) NULL,
	from_wallet_num  varchar(15) NOT NULL,
	to_wallet_num    varchar(15) NOT NULL,
	amount           float8 NOT NULL,
	fee              float8 NOT NULL,
	description      varchar(800) NOT NULL,
	status_code      varchar(10) NOT NULL,
	status           bpchar(1) NOT NULL,
	last_update_date timestamp(6) NOT NULL
);
CREATE INDEX sw_transaction_log_txn_date_idx ON sw_transaction_log USING btree (txn_date, txn_id, reference_id, from_wallet_num, to_wallet_num, txn_type);

--
-- Table structure for table sw_wallet_balance
--
CREATE TABLE sw_wallet_balance (
	wallet_id          varchar(15) NOT NULL,
	card_num           varchar(15) NOT NULL,
	reference_id       varchar(1000) NOT NULL,
	cr                 float8 NOT NULL,
	dr                 float8 NOT NULL,
	balance            float8 NOT NULL,
	last_update_date   timestamp(6) NOT NULL,
	CONSTRAINT sw_wallet_balance_fk FOREIGN KEY (wallet_id) REFERENCES sw_wallet_mast(wallet_id)
);

--
-- Table structure for table sw_wallet_details
--
CREATE TABLE sw_wallet_details (
	wallet_id          varchar(15) NOT NULL,
	account_name       varchar(1000) NOT NULL,
	account_type       varchar(20),
	account_ref        varchar(50),
	balance            float8 NOT NULL,
	last_update_date   timestamp(6) NOT NULL,
	CONSTRAINT sw_wallet_details_fk FOREIGN KEY (wallet_id) REFERENCES public.sw_wallet_mast(wallet_id)
);

--
-- Table structure for table sw_wallet_mast
--
 CREATE TABLE sw_wallet_mast (
	wallet_id         varchar(15) NOT NULL,
	profile_id        varchar(1000) NOT NULL,
	wallet_type       varchar(20) NOT NULL,
	wallet_number     varchar(16) NOT NULL,
	account_no        varchar(25) NOT NULL,
	flg_status        bpchar(1) NOT NULL,
	created_date      date NOT NULL,
	CONSTRAINT sw_wallet_mast_pk PRIMARY KEY (wallet_id)
);
CREATE INDEX sw_wallet_mast_wallet_id_idx ON public.sw_wallet_mast USING btree (wallet_id, profile_id);

--
-- Table structure for table sw_transaction_fdlty_log
--
CREATE TABLE sw_transaction_fdlty_log (
	created_on                        timestamp(6) NOT NULL,
	tran_date                         timestamp(6) NOT NULL,
	request_ref                       varchar(1000) NOT NULL,
	request_typ                       varchar(1000) NOT NULL,
	requester                         varchar(1000) NOT NULL,
	provider                          varchar(1000) NOT NULL,
	status                            varchar(200) NOT NULL,
	narration                         varchar(100) NOT NULL,
	transaction_ref                   varchar(1000) NOT NULL,
	transaction_desc                  varchar(2000) NOT NULL,
	transaction_type                  varchar(1000) NOT NULL,
	session_id                        varchar(1000) NOT NULL,
	payment_reference                 varchar(1000) NOT NULL,
	name_enquiry_ref                  varchar(1000) NOT NULL,
	customer_ref                      varchar(1000) NOT NULL,
	customer_email                    varchar(300) NOT NULL,
	customer_surname                  varchar(500) NOT NULL,
	customer_firstname                varchar(500) NOT NULL,
	customer_mobile_no                varchar(20) NOT NULL,
	tag                               varchar(50) NOT NULL,
	amount                            float8 NOT NULL,
	charge_amount                     float8 NOT NULL,
	from_bank_code                    varchar(10) NOT NULL,
	from_bank_name                    varchar(150) NOT NULL,
	bank_code                         varchar(10) NOT NULL,
	account_number                    varchar(15) NOT NULL,
	cr_account                        varchar(15) NOT NULL,
	cr_account_name                   varchar(50) NOT NULL,
	beneficiary_bvn                   varchar(20) NOT NULL,
	beneficiary_kyc_level             varchar(20) NOT NULL,
	originator_name                   varchar(500) NOT NULL,
	originator_bvn                    varchar(20) NOT NULL,
	originator_cbn_code               varchar(5) NOT NULL,
	originator_kyc_level              varchar(20) NOT NULL,
	transaction_location              varchar(20) NOT NULL,
	collection_account_number         varchar(15) NOT NULL,
	originator_account_number         varchar(15) NOT NULL,
	destination_institution_bank_code varchar(10) NOT null,
	channel_code                      varchar(5) NOT NULL
);
CREATE INDEX sw_transaction_fdlty_log_created_on_idx ON sw_transaction_fdlty_log USING btree (created_on, tran_date, request_ref, request_typ, status, transaction_ref, transaction_type, payment_reference, customer_ref, customer_email, customer_surname, customer_firstname);

--
-- Table structure for table sw_transaction_dispute_fdlty_log
--
CREATE TABLE sw_transaction_dispute_fdlty_log (
	created_on                        timestamp(6) NOT NULL,
	tran_date                         timestamp(6) NOT NULL,
	request_ref                       varchar(1000) NOT NULL,
	request_typ                       varchar(1000) NOT NULL,
	requester                         varchar(1000) NOT NULL,
	provider                          varchar(1000) NOT NULL,
	status                            varchar(200) NOT NULL,
	narration                         varchar(100) NOT NULL,
	transaction_ref                   varchar(1000) NOT NULL,
	transaction_desc                  varchar(2000) NOT NULL,
	transaction_type                  varchar(1000) NOT NULL,
	session_id                        varchar(1000) NOT NULL,
	payment_reference                 varchar(1000) NOT NULL,
	name_enquiry_ref                  varchar(1000) NOT NULL,
	customer_ref                      varchar(1000) NOT NULL,
	customer_email                    varchar(300) NOT NULL,
	customer_surname                  varchar(500) NOT NULL,
	customer_firstname                varchar(500) NOT NULL,
	customer_mobile_no                varchar(20) NOT NULL,
	tag                               varchar(50) NOT NULL,
	amount                            float8 NOT NULL,
	charge_amount                     float8 NOT NULL,
	from_bank_code                    varchar(10) NOT NULL,
	from_bank_name                    varchar(150) NOT NULL,
	bank_code                         varchar(10) NOT NULL,
	account_number                    varchar(15) NOT NULL,
	cr_account                        varchar(15) NOT NULL,
	cr_account_name                   varchar(50) NOT NULL,
	beneficiary_bvn                   varchar(20) NOT NULL,
	beneficiary_kyc_level             varchar(20) NOT NULL,
	originator_name                   varchar(500) NOT NULL,
	originator_bvn                    varchar(20) NOT NULL,
	originator_cbn_code               varchar(5) NOT NULL,
	originator_kyc_level              varchar(20) NOT NULL,
	transaction_location              varchar(20) NOT NULL,
	collection_account_number         varchar(15) NOT NULL,
	originator_account_number         varchar(15) NOT NULL,
	destination_institution_bank_code varchar(10) NOT null,
	channel_code                      varchar(5) NOT NULL
);
CREATE INDEX sw_transaction_dispute_fdlty_log_created_on_idx ON public.sw_transaction_dispute_fdlty_log USING btree (created_on, tran_date, request_ref, request_typ, status, transaction_ref, transaction_type, payment_reference, customer_ref, customer_email, customer_surname, customer_firstname);

--
-- Table structure for table sw_transaction_disburse_log
--
CREATE TABLE sw_transaction_disburse_log (
	disburse_date             timestamp(6) NOT NULL,
	profile_id                varchar(1000) NOT NULL,
	customer_ref              varchar(1000) NOT NULL,
    payment_id                varchar(1000) NOT NULL,
	reference                 varchar(1000) NOT NULL,
	narration                 varchar(2000) NOT NULL,
	fee                       float8 NOT NULL,
	amount                    float8 NOT NULL,
	final_amount              float8 NOT NULL,
	originator_account_name   varchar(1000) NOT NULL,
	originator_account_no     varchar(15) NOT NULL,
	beneficiary_account_name  varchar(1000) NOT NULL,
	beneficiary_account_no    varchar(15) NOT NULL,  
	destination_instite_code  varchar(10) NOT null,
	status                    varchar(200) NOT NULL,
	message                   varchar(1000) NOT NULL,
	provider_response_code    varchar(5) NOT NULL
);
CREATE INDEX sw_transaction_disburse_log_disburse_date_idx ON sw_transaction_disburse_log USING btree (disburse_date, profile_id, customer_ref, payment_id, reference, amount, final_amount, originator_account_no, beneficiary_account_no);

--
-- Function structure for fn_sm_get_seq
--
CREATE OR REPLACE FUNCTION fn_sm_get_seq(v_seq_name varchar(10), v_profile_id varchar(1000))
  RETURNS character varying
  LANGUAGE plpgsql
  AS $function$
    DECLARE v_seq          varchar(30);
    DECLARE v_seq_prx      varchar(30);
    DECLARE v_seq_val      numeric;
    DECLARE v_seq_len      int;
    DECLARE v_seq_count    numeric;
    DECLARE v_seq_1        varchar(50);
    DECLARE v_err_mesg     varchar(32767);
    DECLARE v_err_state    varchar(32767);
  BEGIN
	
	SELECT COUNT(*) From sw_sequence_mast WHERE seq_name = v_seq_name and seq_status ='A' INTO v_seq_count;

        IF v_seq_count = 1 THEN
        
            SELECT seq_value from sw_sequence_mast WHERE seq_name = v_seq_name and seq_status ='A' into v_seq_val;
			
            v_seq_val := v_seq_val + 1;
           
			SELECT seq_length from sw_sequence_mast WHERE seq_name = v_seq_name and seq_status ='A' into v_seq_len;
			SELECT seq_prefix from sw_sequence_mast WHERE seq_name = v_seq_name and seq_status ='A' into v_seq_prx;

			UPDATE sw_sequence_mast 
				SET seq_value = v_seq_val
	       		WHERE seq_name = v_seq_name and seq_status ='A';
	       	
	       	    v_seq_1 := v_seq_val;
                v_seq := concat(v_seq_prx,(select lpad(v_seq_1,v_seq_len,'0')));
			RETURN 	v_seq;
	    ELSE
	   		return '000001';
        END IF;
  EXCEPTION
    WHEN no_data_found THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"fn_sm_get_seq"}',
        'Error Parameters:' || v_seq_name, 'SECURITY');
        COMMIT;
        RETURN '{"status":"Failed","message":"No data found"}';

    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"fn_sm_get_seq"}',
        'Error Parameters:' || v_seq_name, 'SECURITY');
        COMMIT;
        RETURN '{"status":"Failed","message":"Unexpected server error"}';   
 END; $function$;

--
-- Procedure structure for proc_create_country_currency
--
CREATE OR REPLACE PROCEDURE proc_create_country_currency(v_profile_id varchar(1000), v_country_name varchar(50), v_country_code varchar(5), v_currency_code varchar(5), INOUT flag text)
 LANGUAGE plpgsql
 AS $procedure$
 
 DECLARE v_country_id  sw_country_currency.country_id%TYPE; 

 DECLARE v_count            int;
 DECLARE v_err_mesg         varchar(32767);
 DECLARE v_err_state        varchar(32767);

 DECLARE v_result_json    	TEXT;
 BEGIN
	 
	 SELECT count(*) INTO v_count FROM sw_country_currency WHERE country_code = v_country_code;
	 
	   IF v_count = 0 THEN
	    	SELECT fn_sm_get_seq('CountryId', v_profile_id) into v_country_id;

            INSERT INTO sw_country_currency(country_id, country_name, country_code, currency_code, flg_status) 
            VALUES (v_country_id, v_country_name, v_country_code, v_currency_code, 'A');
      

            INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
            VALUES (v_profile_id, CURRENT_TIMESTAMP, 'country_currency created successfully. CountryId:"'||v_country_id||'"', 'CREATE', 'COUNTRYCURRENCY');

            v_result_json := '{"status":"OK","message":"CountryCurrency created successfully","CountryId":"'||v_country_id||'"}';
       
            --Return param
            flag :=v_result_json;
	   ELSE
	        v_result_json := '{"status":"Failed","message":"Country code already exists"}';
       
            --Return param
            flag :=v_result_json;
	   END IF;
      
 EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_create_country_currency"}',
        'Error Parameters:' || v_profile_id, 'COUNTRYCURRENCY');
       
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';
        
       --Return param
        flag :=v_result_json;

 flag := flag;
END $procedure$;

--
-- Procedure structure for proc_get_country_currency
--
CREATE OR REPLACE PROCEDURE proc_get_country_currency(v_profile_id varchar(1000), INOUT flag text)
 LANGUAGE plpgsql
 AS $procedure$

	DECLARE cur_country CURSOR FOR
        SELECT country_id, country_name, country_code, currency_code, flg_status
        FROM  sw_country_currency
        WHERE flg_status = 'A';
       
    DECLARE v_count             int;
	DECLARE v_con_json          TEXT;
    DECLARE v_result_json    	TEXT; 
	DECLARE v_session_count 	INT;
    DECLARE v_err_state         varchar(32767);
 	DECLARE v_err_mesg        	VARCHAR(32767);
  BEGIN
	 
	 SELECT count(*) INTO v_count FROM sw_country_currency WHERE flg_status = 'A';
	
     IF v_count > 0 THEN 
	   	 v_con_json := '[';
	        FOR con IN cur_country LOOP
               v_con_json :=v_con_json
    		   ||	'{"COUNTRY_ID":"'|| con.country_id
               || '","COUNTRY_NAME":"'|| con.country_name
               || '","COUNTRY_CODE":"'|| con.country_code
               || '","CURRENCY_CODE":"'|| con.currency_code
               || '","FLG_STATUS":"'|| con.flg_status
               || '"},';
        	 END LOOP;

             IF v_con_json ='[' THEN
                v_con_json :='[';
             ELSE    
                SELECT substr(v_con_json,0,length(v_con_json) - 0)
                INTO v_con_json;
             END IF;

	         v_con_json := v_con_json || ']';
             v_result_json := ( '{"status":"OK","message":"List of country currencies","countryCurrencyListing":' || v_con_json || '}' );
       
             INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
             VALUES (v_profile_id, CURRENT_TIMESTAMP, 'List of countryCurrency retrieved by profileId:"'||v_profile_id||'"', 'GET', 'COUNTRYCURRENCY');
       
             --Return param
             flag :=v_result_json;
	ELSE
        	 v_result_json := '{"status":"Failed","message":"Currency is not configured"}';
       
             --Return param
             flag :=v_result_json;
	END IF;
     
 EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_get_country_currency"}',
        'Error Parameters:' || v_profile_id, 'COUNTRYCURRENCY');
       
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';

        --Return param
        flag :=v_result_json;
       
       
 flag :=v_result_json;
END $procedure$;

--
-- Procedure structure for proc_create_fee
--
CREATE OR REPLACE PROCEDURE proc_create_fee(v_profile_id varchar(1000), v_fee_name varchar(50), v_fee_type varchar(20), v_conv_fee double precision, v_tnx_fee double precision, INOUT flag text)
 LANGUAGE plpgsql
 AS $procedure$

 DECLARE v_fee_id           public.sw_fees.fees_id%TYPE; 

 DECLARE v_count            int;
 DECLARE v_err_mesg         varchar(32767);
 DECLARE v_err_state        varchar(32767);

 DECLARE v_result_json      TEXT;
 BEGIN
	  
	  	 SELECT count(*) INTO v_count FROM sw_fees WHERE fee_convenience = 0 AND fee_transaction = 0 AND flg_status = 'A';
	  
	     IF v_count = 0 THEN
	          SELECT fn_sm_get_seq('FeeId', v_profile_id) into v_fee_id;

              INSERT INTO sw_fees(fees_id, fee_name, fee_type, fee_convenience, fee_transaction, flg_status) 
              VALUES (v_fee_id, v_fee_name, v_fee_type, v_conv_fee, v_tnx_fee, 'A');
      

             INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
             VALUES (v_profile_id, CURRENT_TIMESTAMP, 'country_currency created successfully. FeeId:"'||v_fee_id||'"', 'CREATE', 'FEES');

             v_result_json := '{"status":"OK","message":"Fee created successfully","FeeId":"'||v_fee_id||'"}';
       
             --Return param
             flag :=v_result_json;
	     ELSE
	         v_result_json := '{"status":"Failed","message":"Fees already exists"}';
       
             --Return param
             flag :=v_result_json;
	     END IF;
      
 EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_create_fee"}',
        'Error Parameters:' || v_profile_id, 'FEES');
       
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';
       
        --Return param
        flag :=v_result_json;

 flag := flag;
END $procedure$;

--
-- Procedure structure for proc_get_fee
--
CREATE OR REPLACE PROCEDURE proc_get_fee(v_profile_id varchar(1000), INOUT flag text)
 LANGUAGE plpgsql
 AS $procedure$

	DECLARE cur_fees CURSOR FOR
        SELECT fees_id, fee_name, fee_type, 
               fee_convenience, fee_transaction, flg_status
        FROM sw_fees
        WHERE flg_status = 'A';
       
    DECLARE v_count             int;
	DECLARE v_fees_json         TEXT;
    DECLARE v_result_json    	TEXT; 
	DECLARE v_session_count 	INT;
    DECLARE v_err_state         varchar(32767);
 	DECLARE v_err_mesg        	VARCHAR(32767);
 BEGIN
	 
	 	 SELECT count(*) INTO v_count FROM sw_fees WHERE flg_status = 'A';
	
	      IF v_count > 0 THEN 
	         v_fees_json := '[';
	         FOR fees IN cur_fees LOOP
                v_fees_json :=v_fees_json
	        	   ||	'{"FEES_ID":"'|| fees.fees_id
                   || '","FEE_NAME":"'|| fees.fee_name
                   || '","FEE_TYPE":"'|| fees.fee_type
                   || '","CONVENIENCE_FEE":"'|| fees.fee_convenience
                   || '","TRANSACTION_FEE":"'|| fees.fee_transaction
                   || '","FLG_STATUS":"'|| fees.flg_status
                   || '"},';
         	 END LOOP;

             IF v_fees_json ='[' THEN
                v_fees_json :='[';
             ELSE    
                SELECT substr(v_fees_json,0,length(v_fees_json) - 0)
                 INTO v_fees_json;
                END IF;

	           v_fees_json := v_fees_json || ']';
               v_result_json := ( '{"status":"OK","message":"List of fee(s)","feesListing":' || v_fees_json || '}' );
       
                INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
                VALUES (v_profile_id, CURRENT_TIMESTAMP, 'List of fees retrieved by profileId:"'||v_profile_id||'"', 'GET', 'FEES');
       
               --Return param
                 flag :=v_result_json;
	       ELSE
        	 v_result_json := '{"status":"Failed","message":"Fee is not configured"}';
       
             --Return param
             flag :=v_result_json;
	        END IF;
     
 EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_get_fee"}',
        'Error Parameters:' || v_profile_id, 'Fees');
       
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';
 
        --Return param
        flag :=v_result_json;       
       
 flag :=v_result_json;
END $procedure$;

--
-- Procedure structure for proc_create_txn
--
CREATE OR REPLACE PROCEDURE proc_create_txn(v_txn_ref varchar(1000), v_from_wallet_num varchar(30), v_to_wallet_num varchar(30), v_amount double precision, v_txn_fee double precision, v_conv_fee double precision, v_description varchar(800), v_txn_type varchar(50), INOUT flag text)
 LANGUAGE plpgsql
 AS $procedure$
 DECLARE v_txn_id                 sw_transaction_log.txn_id%TYPE;
 DECLARE v_profile_id             sw_transaction_log.profile_id%TYPE; 
 
 DECLARE v_from_wallet_id         sw_wallet_mast.wallet_id%TYPE;
 DECLARE v_to_wallet_id           sw_wallet_mast.wallet_id%TYPE;

 DECLARE v_from_current_bal       sw_wallet_details.balance%TYPE;
 DECLARE v_to_current_bal         sw_wallet_details.balance%TYPE;

 DECLARE n_from_current_bal       sw_wallet_details.balance%TYPE;
 DECLARE n_to_current_bal         sw_wallet_details.balance%TYPE;
 DECLARE n_fee                    sw_transaction_log.fee%TYPE;
 DECLARE n_amount                 sw_transaction_log.amount%TYPE;

 DECLARE v_wallet_count           int;
 DECLARE v_ref_count              int;

 DECLARE v_err_mesg               varchar(32767);
 DECLARE v_err_state              varchar(32767);

 DECLARE v_result_json            TEXT;
 BEGIN
	 
	 SELECT count(*) INTO v_ref_count FROM sw_transaction_log WHERE reference_id = v_txn_ref;
	
	  IF v_ref_count = 0 THEN 
	    	 SELECT count(*) INTO v_wallet_count FROM sw_wallet_mast WHERE wallet_number = v_from_wallet_num;
	 
    	     IF v_wallet_count > 0 THEN
                    SELECT profile_id INTO v_profile_id FROM sw_wallet_mast WHERE wallet_number = v_from_wallet_num;
	     	
        	        SELECT wallet_id INTO v_from_wallet_id FROM sw_wallet_mast WHERE wallet_number = v_from_wallet_num;
        	        SELECT wallet_id INTO v_to_wallet_id FROM sw_wallet_mast WHERE wallet_number = v_to_wallet_num;
	     	
        	        SELECT balance INTO v_from_current_bal FROM sw_wallet_details WHERE wallet_id = v_from_wallet_id;
        	        SELECT balance INTO v_to_current_bal FROM sw_wallet_details WHERE wallet_id = v_to_wallet_id;
	    
	        
        	         n_from_current_bal := (v_from_current_bal - v_amount);
            	     n_to_current_bal := (v_to_current_bal + v_amount);
	    
        	         n_fee := (v_txn_fee + v_conv_fee);
        	         n_amount := (v_amount - n_fee);
	    
       	            IF v_from_current_bal > v_amount THEN 
	     
        	               SELECT fn_sm_get_seq('TranxId', v_profile_id) into v_txn_id;
                
                           INSERT INTO sw_transaction_log(txn_date, txn_id, profile_id, reference_id, from_wallet_num, to_wallet_num, last_update_date, amount, fee, description, status_code, status, txn_type) 
                           VALUES (CURRENT_TIMESTAMP, v_txn_id, v_profile_id, v_txn_ref, v_from_wallet_num, v_to_wallet_num, CURRENT_TIMESTAMP, v_amount, n_fee, v_description, '00', 'A', v_txn_type);
               
                           INSERT INTO sw_wallet_balance(wallet_id, reference_id, card_num, cr, dr, balance, last_update_date)
                           VALUES (v_from_wallet_id, v_txn_ref, v_from_wallet_num, 0, v_amount, n_from_current_bal, CURRENT_TIMESTAMP);
               
                           INSERT INTO sw_wallet_balance(wallet_id, reference_id, card_num, cr, dr, balance, last_update_date)
                           VALUES (v_to_wallet_id, v_txn_ref, v_to_wallet_num, n_amount, 0, n_to_current_bal, CURRENT_TIMESTAMP);
               
                           UPDATE sw_wallet_details
                              SET balance = n_from_current_bal,
                                  last_update_date = CURRENT_TIMESTAMP
                            WHERE wallet_id = v_from_wallet_id;	
               
                           UPDATE sw_wallet_details
                              SET balance = n_to_current_bal,
                                  last_update_date = CURRENT_TIMESTAMP
                            WHERE wallet_id = v_to_wallet_id;
               
                            INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
                            VALUES (v_profile_id, CURRENT_TIMESTAMP, 'Transaction created successfully. TxnId:"'||v_txn_id||'"', 'CREATE', 'TRANSACTION');
  
                            v_result_json := '{"status":"OK","message":"Transaction created successfully","TxnRef":"'||v_txn_ref||'","TxnId":"'||v_txn_id||'"}';
       
                            --Return param
                           flag :=v_result_json;
    	            ELSE
    	                 v_result_json := '{"status":"Failed","message":"Insufficient funds"}';
       
                         --Return param
                         flag :=v_result_json;
    	            END IF;
	    
    	     ELSE 
    	         v_result_json := '{"status":"Failed","message":"Invalid card number"}';
       
                  --Return param
                 flag :=v_result_json;
    	     END IF;
	  ELSE 
	     v_result_json := '{"status":"Failed","message":"Duplicate transaction reference"}';
       
          --Return param
          flag :=v_result_json;
	  END IF;
  EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_create_txn"}',
        'Error Parameters:' || v_profile_id, 'TRANSACTION');
       
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';
       
        --Return param
        flag :=v_result_json;
       
 flag :=v_result_json;
END;$procedure$;

--
-- Procedure structure for proc_get_tnx
--
CREATE OR REPLACE PROCEDURE proc_get_tnx(v_profile_id varchar(1000), INOUT flag text)
 LANGUAGE plpgsql
 AS $procedure$

	DECLARE cur_tnx CURSOR FOR
        SELECT tnx.txn_date, tnx.txn_id, tnx.reference_id, tnx.txn_type, 
               tnx.from_wallet_num, tnx.to_wallet_num, tnx.amount, tnx.description, tnx.status_code  
        FROM sw_transaction_log tnx
        WHERE tnx.profile_id = v_profile_id;
       
    DECLARE v_count             int;
	DECLARE v_tnx_json          TEXT;
    DECLARE v_result_json    	TEXT; 
	DECLARE v_session_count 	INT;
    DECLARE v_err_state         varchar(32767);
 	DECLARE v_err_mesg        	VARCHAR(32767);
 BEGIN
	 	 SELECT count(*) INTO v_count FROM sw_transaction_log WHERE profile_id = v_profile_id;
	
          IF v_count > 0 THEN 
            	 v_tnx_json := '[';
            	 FOR tnxs IN cur_tnx LOOP
                     v_tnx_json :=v_tnx_json
            		   ||	'{"TXN_DATE":"'|| tnxs.txn_date
                       || '","TXN_ID":"'|| tnxs.txn_id
                       || '","TXN_TYPE":"'|| COALESCE(tnxs.txn_type,'')
                       || '","REFERENCE_ID":"'|| tnxs.reference_id
                       || '","FROM_WALLET_NUMBER":"'|| tnxs.from_wallet_num
                       || '","TO_WALLET_NUMBER":"'|| tnxs.to_wallet_num
                       || '","AMOUNT":"'|| tnxs.amount
                       || '","DESCRIPTION":"'|| tnxs.description
                       || '","STATUS_CODE":"'|| tnxs.status_code
                       || '"},';
             	 END LOOP;

                 IF v_tnx_json ='[' THEN
                    v_tnx_json :='[';
                 ELSE    
                    SELECT substr(v_tnx_json,0,length(v_tnx_json) - 0)
                    INTO v_tnx_json;
                 END IF;

	             v_tnx_json := v_tnx_json || ']';
                 v_result_json := ( '{"status":"OK","message":"List of user transaction(s)","transactionListing":' || v_tnx_json || '}' );
       
                 INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
                 VALUES (v_profile_id, CURRENT_TIMESTAMP, 'List of transaction retrieved by profileId:"'||v_profile_id||'"', 'GET', 'TRANSACTION');
       
                  --Return param
                  flag :=v_result_json;
	       ELSE
        	      v_result_json := '{"status":"Failed","message":"No transaction(s) available for user"}';
       
                   --Return param
                  flag :=v_result_json;
	       END IF;
     
 EXCEPTION
        WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_get_tnx"}',
        'Error Parameters:' || v_profile_id, 'TRANSACTION');
       
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';

        --Return param
        flag :=v_result_json;
       
 flag :=v_result_json;
END $procedure$;

--
-- Procedure structure for proc_get_tnx_details
--
CREATE OR REPLACE PROCEDURE proc_get_tnx_details(v_ref_id varchar(30), INOUT flag text)
 LANGUAGE plpgsql
 AS $procedure$

        DECLARE cur_tnx CURSOR FOR
        SELECT wb.wallet_id, wb.card_num, wb.reference_id, wb.cr, wb.dr, 
               wb.balance, wb.last_update_date
        FROM sw_wallet_balance wb
        WHERE wb.reference_id = v_ref_id;

    DECLARE v_count             int;
	DECLARE v_tnx_json          TEXT;
    DECLARE v_result_json    	TEXT; 
	DECLARE v_session_count 	INT;
    DECLARE v_profile_id        sw_wallet_mast.profile_id%TYPE;
    DECLARE v_err_state         varchar(32767);
 	DECLARE v_err_mesg        	VARCHAR(32767);
  BEGIN
	    SELECT count(*) INTO v_count FROM sw_wallet_balance WHERE reference_id = v_ref_id;
	    
	    IF v_count > 0 THEN 	     
	     
	        SELECT profile_id INTO v_profile_id
	        FROM sw_wallet_mast 
	        WHERE wallet_id = (SELECT wallet_id 
	                           FROM sw_wallet_balance 
	                           WHERE reference_id = v_ref_id AND cr = 0);
	     
         	 v_tnx_json := '[';
            	 FOR tnxs IN cur_tnx LOOP
                     v_tnx_json :=v_tnx_json
            		   ||	'{"WALLET_ID":"'|| tnxs.wallet_id
                       || '","CARD_NUM":"'|| tnxs.card_num
                       || '","REFERENCE_ID":"'|| tnxs.reference_id
                       || '","CR":"'|| tnxs.cr
                       || '","DR":"'|| tnxs.dr
                       || '","BALANCE":"'|| tnxs.balance
                       || '","LAST_UPDATE_DATE":"'|| tnxs.last_update_date
                       || '"},';
               	 END LOOP;

                 IF v_tnx_json ='[' THEN
                    v_tnx_json :='[';
                 ELSE    
                   SELECT substr(v_tnx_json,0,length(v_tnx_json) - 0)
                     INTO v_tnx_json;
                 END IF;

	          v_tnx_json := v_tnx_json || ']';
              v_result_json := ( '{"status":"OK","message":"Transaction details","transactionDetails":' || v_tnx_json || '}' );
       
               INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
               VALUES (v_profile_id, CURRENT_TIMESTAMP, 'Transaction details retrieved by refId:"'||v_ref_id||'"', 'GET', 'TRANSACTION');
       
               --Return param
               flag :=v_result_json;
	      ELSE
	           v_result_json := '{"status":"Failed","message":"Transaction details does not exist"}';
       
                --Return param
               flag :=v_result_json;
	      END IF;
     
 EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_get_tnx_details"}',
        'Error Parameters:' || v_profile_id, 'TRANSACTION');
       
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';
       
        --Return param
        flag :=v_result_json;       
       
 flag :=v_result_json;
END $procedure$;

--
-- Procedure structure for proc_create_wallet
--
CREATE OR REPLACE PROCEDURE proc_create_wallet(v_profile_id varchar(1000), v_country_code varchar(5), v_account_no varchar(25), v_account_ref varchar(50), v_account_name varchar(1000), v_account_type varchar(20), v_bank_name varchar(20), v_bank_code varchar(5), INOUT flag text)
  LANGUAGE plpgsql
  AS $procedure$
  DECLARE v_wallet_id           sw_wallet_mast.wallet_id%TYPE;
  DECLARE v_wallet_num          sw_wallet_mast.wallet_number%TYPE; 

  DECLARE v_wallet_type         sw_wallet_mast.wallet_type%TYPE;  
  DECLARE v_wallet_number       sw_wallet_mast.wallet_type%TYPE;
  DECLARE x_account_no          sw_wallet_mast.account_no%TYPE;
  DECLARE x_bank_name           sw_wallet_mast.bank_name%TYPE;
  DECLARE x_bank_code           sw_wallet_mast.bank_code%TYPE;
  DECLARE v_balance             sw_wallet_details.balance%TYPE; 
  DECLARE v_flg_status          sw_wallet_mast.flg_status%TYPE; 
  DECLARE v_last_update_date    sw_wallet_details.last_update_date%TYPE; 

  DECLARE v_wallet_count        int;
  DECLARE v_currency_count      int;

  DECLARE v_err_mesg            varchar(32767);
  DECLARE v_err_state           varchar(32767);

  DECLARE v_wallet_json         TEXT;
  DECLARE v_result_json         TEXT;

  BEGIN
	 
	 SELECT count(*) INTO v_currency_count FROM sw_country_currency WHERE country_code  = v_country_code;
             
     IF v_currency_count > 0 THEN 
            
           SELECT count(*) INTO v_wallet_count FROM public.sw_wallet_mast WHERE profile_id = v_profile_id AND wallet_type = v_country_code;
   
            IF v_wallet_count > 0 THEN 
            
               	SELECT wm.wallet_type, wm.wallet_number, wm.account_no,
                       wd.balance, wm.flg_status, wd.last_update_date,
                       wm.bank_code, wm.bank_name
                INTO v_wallet_type, v_wallet_number, x_account_no,
        	         v_balance, v_flg_status, v_last_update_date, 
        	         x_bank_code, x_bank_name
                FROM sw_wallet_mast wm
                INNER JOIN sw_wallet_details wd ON wm.wallet_id = wd.wallet_id
                WHERE wm.profile_id = v_profile_id
                AND wm.wallet_type = v_country_code;
        

                v_wallet_json := '{"WALLET_ID":"'|| v_wallet_id
        		   || '","PROFILE_ID":"'|| v_profile_id
                   || '","WALLET_TYPE":"'||  v_wallet_type
                   || '","WALLET_NUMBER":"'|| v_wallet_number
                   || '","BANK_CODE":"'|| x_bank_code
                   || '","BANK_NAME":"'|| x_bank_name
                   || '","ACCOUNT_NUMBER":"'|| x_account_no
                   || '","BALANCE":"'|| v_balance
                   || '","FLG_STATUS":"'|| v_flg_status
                   || '"}';

                  v_result_json := ( '{"status":"OK","message":"Wallet already exists","walletDetails":' || v_wallet_json || '}' );
                  --Return param
                 flag :=v_result_json;
            ELSE 
            
                 SELECT fn_sm_get_seq('WalletId', v_profile_id) into v_wallet_id;
                 SELECT fn_sm_get_seq('CardNum', v_profile_id) into v_wallet_num;
                
                 v_wallet_num:= concat(v_country_code, v_wallet_num);


                 INSERT INTO sw_wallet_mast(wallet_id, profile_id, wallet_type, wallet_number, account_no, bank_code, bank_name, flg_status, created_date) 
                 VALUES (v_wallet_id, v_profile_id, v_country_code, v_wallet_num, v_account_no, v_bank_code, v_bank_name, 'A', CURRENT_TIMESTAMP);
      
                 INSERT INTO sw_wallet_details(wallet_id, account_name, account_type, account_ref, balance, last_update_date) 
                 VALUES (v_wallet_id, v_account_name, v_account_type, v_account_ref, 0, CURRENT_TIMESTAMP);	

                 INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
                 VALUES (v_profile_id, CURRENT_TIMESTAMP, 'Wallet created successfully. WalletId:"'||v_wallet_id||'"', 'CREATE', 'WALLET');
           
                 v_wallet_json := '{"WALLET_ID":"'|| v_wallet_id
        		     || '","PROFILE_ID":"'|| v_profile_id
                     || '","WALLET_TYPE":"'||  v_country_code
                     || '","WALLET_NUMBER":"'|| v_wallet_num
                     || '","BANK_CODE":"'|| v_bank_code
                     || '","BANK_NAME":"'|| v_bank_name
                     || '","ACCOUNT_NUMBER":"'|| v_account_no
                     || '","BALANCE":"'|| 0
                     || '","FLG_STATUS":"'|| 'A'
                     || '"}';

                 v_result_json := ( '{"status":"OK","message":"Wallet created successfully","walletDetails":' || v_wallet_json || '}' );
       
                  --Return param
                  flag :=v_result_json;
            END IF;
     ELSE
         v_result_json := ( '{"status":"Failed","message":"Wallet currency not supported"}' );
       
          --Return param
        flag :=v_result_json;
     END IF;  
  EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_create_wallet"}',
        'Error Parameters:' || v_profile_id, 'Wallet');
       
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';
 
        --Return param
        flag :=v_result_json;

  flag := flag;
END $procedure$;

--
-- Procedure structure for proc_get_wallet
--
CREATE OR REPLACE PROCEDURE proc_get_wallet(v_profile_id varchar(1000), INOUT flag text)
 LANGUAGE plpgsql
 AS $procedure$

	DECLARE cur_wallet CURSOR FOR
        SELECT wm.wallet_id, wm.wallet_type, wm.wallet_number, 
               wd.balance, wm.flg_status, wd.last_update_date
        FROM sw_wallet_mast wm
        INNER JOIN sw_wallet_details wd ON wm.wallet_id = wd.wallet_id
        WHERE wm.profile_id = v_profile_id;
       
    DECLARE v_count             int;
	DECLARE v_wallet_json       TEXT;
    DECLARE v_result_json    	TEXT; 
	DECLARE v_session_count 	INT;
    DECLARE v_err_state         varchar(32767);
 	DECLARE v_err_mesg        	VARCHAR(32767);
 BEGIN
	 
	 SELECT count(*) INTO v_count FROM sw_wallet_mast WHERE profile_id = v_profile_id;
	    
	 IF v_count > 0 then 
	     	 v_wallet_json := '[';
	           FOR wallet IN cur_wallet LOOP
                   v_wallet_json :=v_wallet_json
        		   ||	'{"WALLET_ID":"'|| wallet.wallet_id
                   || '","WALLET_TYPE":"'|| wallet.wallet_type
                   || '","WALLET_NUMBER":"'|| wallet.wallet_number
                   || '","BALANCE":"'|| wallet.balance
                   || '","FLG_STATUS":"'|| wallet.flg_status
                   || '","LAST_UPDATE_DATE":"'|| wallet.last_update_date
                   || '"},';
               END LOOP;

               IF v_wallet_json ='[' THEN
                  v_wallet_json :='[';
               ELSE    
                  SELECT substr(v_wallet_json,0,length(v_wallet_json) - 0)
                  INTO v_wallet_json;
               END IF;

	        v_wallet_json := v_wallet_json || ']';
            v_result_json := ( '{"status":"OK","message":"List of user wallet(s)","walletListing":' || v_wallet_json || '}' );
       
            INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
            VALUES (v_profile_id, CURRENT_TIMESTAMP, 'List of wallet retrieved by profileId:"'||v_profile_id||'"', 'GET', 'WALLET');
       
            --Return param
            flag :=v_result_json;
	 ELSE
	       v_result_json := '{"status":"Failed","message":"Wallet does not exist"}';
       
           --Return param
           flag :=v_result_json;
	 END IF;
     
 EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_get_wallet"}',
        'Error Parameters:' || v_profile_id, 'Wallet');
       
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';
       
         --Return param
        flag :=v_result_json;
       
       
 flag :=v_result_json;
END $procedure$;

--
-- Procedure structure for proc_get_wallet_details
--
CREATE OR REPLACE PROCEDURE proc_get_wallet_details(v_wallet_id varchar(30), INOUT flag text)
 LANGUAGE plpgsql
 AS $procedure$
 
    DECLARE v_profile_id        sw_wallet_mast.profile_id%TYPE; 
    DECLARE v_wallet_type       sw_wallet_mast.wallet_type%TYPE;  
    DECLARE v_wallet_number     sw_wallet_mast.wallet_type%TYPE; 
    DECLARE v_balance           sw_wallet_details.balance%TYPE; 
    DECLARE v_flg_status        sw_wallet_mast.flg_status%TYPE; 
    DECLARE v_last_update_date  sw_wallet_details.last_update_date%TYPE; 
   
    DECLARE v_count             int;
	DECLARE v_wallet_json       TEXT;
    DECLARE v_result_json    	TEXT; 
    DECLARE v_err_state         varchar(32767);
 	DECLARE v_err_mesg        	VARCHAR(32767);
 BEGIN 
	 
	    SELECT count(*) INTO v_count FROM sw_wallet_mast WHERE wallet_id = v_wallet_id;
	    
	    IF v_count > 0 THEN 
	             
	       SELECT wm.profile_id INTO v_profile_id 
	       FROM sw_wallet_mast wm 
	       INNER JOIN sw_wallet_details wd ON wm.wallet_id = wd.wallet_id
           WHERE wm.wallet_id = v_wallet_id;
                
	       SELECT wm.wallet_type, wm.wallet_number, wd.balance, wm.flg_status, wd.last_update_date
           INTO v_wallet_type, v_wallet_number, v_balance, v_flg_status, v_last_update_date
           FROM sw_wallet_mast wm
           INNER JOIN sw_wallet_details wd ON wm.wallet_id = wd.wallet_id
           WHERE wm.wallet_id = v_wallet_id;
        

           v_wallet_json := '{"WALLET_ID":"'|| v_wallet_id
                 || '","PROFILE_ID":"'|| v_profile_id
                 || '","WALLET_TYPE":"'||  v_wallet_type
                 || '","WALLET_NUMBER":"'|| v_wallet_number
                 || '","BALANCE":"'|| v_balance
                 || '","FLG_STATUS":"'|| v_flg_status
                 || '","LAST_UPDATE_DATE":"'|| v_last_update_date
                 || '"}';

           v_result_json := ( '{"status":"OK","message":"Wallet details","walletDetail":' || v_wallet_json || '}' );
       
           INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
           VALUES (v_profile_id, CURRENT_TIMESTAMP, 'Wallet details retrieved for walletId:"'||v_wallet_id||'"', 'GET', 'WALLET');
       
           --Return param
           flag :=v_result_json;
	    ELSE
	       v_result_json := '{"status":"Failed","message":"Wallet details does not exist"}';
       
           --Return param
           flag :=v_result_json;
	     END IF;
	       
 EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_get_wallet_details"}',
        'Error Parameters:' || v_profile_id, 'Wallet');
       
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';
       
         --Return param
        flag :=v_result_json;
       
  flag :=v_result_json;     
END $procedure$;

--
-- Procedure structure for proc_update_wallet
--
CREATE OR REPLACE PROCEDURE proc_update_wallet(v_wallet_id varchar(30), v_ststus_code char(1), INOUT flag text)
  LANGUAGE plpgsql
  AS $procedure$

  DECLARE v_profile_id      sw_wallet_mast.profile_id%TYPE;
  DECLARE v_wallet_count    int;

  DECLARE v_err_mesg     varchar(32767);
  DECLARE v_err_state    varchar(32767);

  DECLARE v_result_json    	TEXT;
  BEGIN
	  SELECT COUNT(*) INTO v_wallet_count
      FROM sw_wallet_mast
      WHERE wallet_id = v_wallet_id;
     
      IF  v_wallet_count = 1 THEN 
      
         SELECT profile_id FROM sw_wallet_mast WHERE wallet_id = v_wallet_id INTO v_profile_id; 
        
         UPDATE sw_wallet_mast	
		    SET flg_status = v_ststus_code
		 WHERE wallet_id = v_wallet_id;

		 INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
         VALUES (v_profile_id, CURRENT_TIMESTAMP, 'Wallet updated successfully. WalletId:"'||v_wallet_id||'"', 'UPDATE', 'WALLET');
       
          v_result_json := '{"status":"OK","message":"Updated wallet successfully,"WalletId":"'||v_wallet_id||'"}';

          --Return param
          flag :=v_result_json;
       ELSE
          v_result_json := '{"status":"Failed","message":"Wallet Does Not Exist"}';
          --Return param
          flag :=v_result_json;
       END IF;
      
  EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_update_wallet"}',
        'Error Parameters:' || v_profile_id, 'Wallet');
       
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';
       
        --Return param
        flag :=v_result_json;

  flag :=v_result_json; 
END $procedure$;

--
-- Procedure structure for proc_create_txn_fdlty
--
CREATE OR REPLACE PROCEDURE proc_create_txn_fdlty(v_req_ref varchar, v_req_type varchar, v_requester varchar, v_status varchar, v_provider varchar, v_trx_ref varchar, v_trx_desc varchar, v_trx_type varchar, v_cust_ref varchar, v_cust_email varchar, v_cust_surname varchar, v_cust_fname varchar, v_cust_mobile_no varchar, v_tag varchar, v_amt double precision, v_from_bnk_code varchar, v_bnk_name varchar, v_bnk_code varchar, v_cr_account varchar, v_narration varchar, v_session_id varchar, v_channel_code varchar, v_charge_amt double precision, v_cr_acc_name varchar, v_orign_bvn varchar, v_acc_no varchar, v_benef_bvn varchar, v_enq_ref_name varchar, v_orign_name varchar, v_pay_ref varchar, v_orign_cbn_code varchar, v_orign_kyc_level varchar, v_ben_kyc_level varchar, v_tran_loc varchar, v_coll_acc_no varchar, v_orign_acc_no varchar, v_dest_inst_bank_code varchar)
 LANGUAGE plpgsql
 AS $procedure$
  
 DECLARE v_wallet_id              sw_wallet_mast.wallet_id%TYPE;
 DECLARE v_wallet_number          sw_wallet_mast.wallet_number%TYPE;
 DECLARE v_current_bal            sw_wallet_details.balance%TYPE;
 DECLARE n_new_bal                sw_wallet_details.balance%TYPE;

 DECLARE v_acc_count              int;
 DECLARE v_tran_count             int;
 DECLARE v_dis_tran_count         int;

 DECLARE v_err_mesg               varchar(32767);
 DECLARE v_err_state              varchar(32767);

  BEGIN
	       IF LOWER(v_status) ='failed' THEN 
               INSERT INTO sw_transaction_dispute_fdlty_log(request_ref, request_typ, requester, status, provider, transaction_ref, transaction_desc, transaction_type, customer_ref, customer_email, customer_surname, customer_firstname, customer_mobile_no, tag, amount, from_bank_code, from_bank_name, tran_date, bank_code, cr_account, created_on, narration, session_id, channel_code, charge_amount, cr_account_name, originator_bvn, account_number, beneficiary_bvn, name_enquiry_ref, originator_name, payment_reference, originator_cbn_code, originator_kyc_level, beneficiary_kyc_level, transaction_location, collection_account_number, originator_account_number, destination_institution_bank_code) 
               VALUES (v_req_ref, v_req_type, v_requester, v_status, v_provider, v_trx_ref, v_trx_desc, v_trx_type, v_cust_ref, v_cust_email, v_cust_surname, v_cust_fname, v_cust_mobile_no, v_tag, v_amt, v_from_bnk_code, v_bnk_name, CURRENT_TIMESTAMP, v_bnk_code, v_cr_account, CURRENT_TIMESTAMP, v_narration, v_session_id, v_channel_code, v_charge_amt, v_cr_acc_name, v_orign_bvn, v_acc_no, v_benef_bvn, v_enq_ref_name, v_orign_name, v_pay_ref, v_orign_cbn_code, v_orign_kyc_level, v_ben_kyc_level, v_tran_loc, v_coll_acc_no, v_orign_acc_no, v_dest_inst_bank_code);
                      
               INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
               VALUES (v_session_id, CURRENT_TIMESTAMP, 'Customer transaction failed. TransactionRef:"'||v_trx_ref||'"', 'CREATE', 'DISPUTE - WEBHOOK TRANSACTION');	           
	       ELSE 
         	   SELECT count(*) INTO v_dis_tran_count FROM sw_transaction_dispute_fdlty_log 
               WHERE payment_reference = v_pay_ref AND transaction_ref = v_trx_ref AND amount = v_amt;
              
               IF v_dis_tran_count = 0 THEN 
               
                   SELECT count(*) INTO v_tran_count FROM sw_transaction_fdlty_log 
                   WHERE payment_reference = v_pay_ref AND transaction_ref = v_trx_ref AND amount = v_amt;
                  
                   IF v_tran_count = 0 THEN 
                   
                       SELECT count(*) INTO v_acc_count FROM sw_wallet_mast WHERE account_no = v_cr_account;
                   
                       IF v_acc_count = 1 THEN 
                           SELECT wallet_id, wallet_number INTO v_wallet_id, v_wallet_number 
                           FROM sw_wallet_mast WHERE account_no = v_cr_account;
                     
                          SELECT balance INTO v_current_bal FROM sw_wallet_details WHERE wallet_id = v_wallet_id;
                     
                          n_new_bal := (v_current_bal + v_amt);
                      
                         
                          INSERT INTO sw_transaction_fdlty_log(request_ref, request_typ, requester, status, provider, transaction_ref, transaction_desc, transaction_type, customer_ref, customer_email, customer_surname, customer_firstname, customer_mobile_no, tag, amount, from_bank_code, from_bank_name, tran_date, bank_code, cr_account, created_on, narration, session_id, channel_code, charge_amount, cr_account_name, originator_bvn, account_number, beneficiary_bvn, name_enquiry_ref, originator_name, payment_reference, originator_cbn_code, originator_kyc_level, beneficiary_kyc_level, transaction_location, collection_account_number, originator_account_number, destination_institution_bank_code) 
                          VALUES (v_req_ref, v_req_type, v_requester, v_status, v_provider, v_trx_ref, v_trx_desc, v_trx_type, v_cust_ref, v_cust_email, v_cust_surname, v_cust_fname, v_cust_mobile_no, v_tag, v_amt, v_from_bnk_code, v_bnk_name, CURRENT_TIMESTAMP, v_bnk_code, v_cr_account, CURRENT_TIMESTAMP, v_narration, v_session_id, v_channel_code, v_charge_amt, v_cr_acc_name, v_orign_bvn, v_acc_no, v_benef_bvn, v_enq_ref_name, v_orign_name, v_pay_ref, v_orign_cbn_code, v_orign_kyc_level, v_ben_kyc_level, v_tran_loc, v_coll_acc_no, v_orign_acc_no, v_dest_inst_bank_code);
                      
                          INSERT INTO sw_wallet_balance(wallet_id, reference_id, card_num, cr, dr, balance, last_update_date)
                          VALUES (v_wallet_id, v_pay_ref, v_wallet_number, v_amt, 0, n_new_bal, CURRENT_TIMESTAMP);
                      
                          UPDATE sw_wallet_details
                             SET balance = n_new_bal,
                                 last_update_date = CURRENT_TIMESTAMP
                           WHERE wallet_id = v_wallet_id;	
                       
                          INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
                          VALUES (v_session_id, CURRENT_TIMESTAMP, 'User credited successfully. TransactionRef:"'||v_trx_ref||'"', 'CREATE', 'WEBHOOK TRANSACTION');
                       ELSE  
                         
                          INSERT INTO sw_transaction_dispute_fdlty_log(request_ref, request_typ, requester, status, provider, transaction_ref, transaction_desc, transaction_type, customer_ref, customer_email, customer_surname, customer_firstname, customer_mobile_no, tag, amount, from_bank_code, from_bank_name, tran_date, bank_code, cr_account, created_on, narration, session_id, channel_code, charge_amount, cr_account_name, originator_bvn, account_number, beneficiary_bvn, name_enquiry_ref, originator_name, payment_reference, originator_cbn_code, originator_kyc_level, beneficiary_kyc_level, transaction_location, collection_account_number, originator_account_number, destination_institution_bank_code) 
                          VALUES (v_req_ref, v_req_type, v_requester, v_status, v_provider, v_trx_ref, v_trx_desc, v_trx_type, v_cust_ref, v_cust_email, v_cust_surname, v_cust_fname, v_cust_mobile_no, v_tag, v_amt, v_from_bnk_code, v_bnk_name, CURRENT_TIMESTAMP, v_bnk_code, v_cr_account, CURRENT_TIMESTAMP, v_narration, v_session_id, v_channel_code, v_charge_amt, v_cr_acc_name, v_orign_bvn, v_acc_no, v_benef_bvn, v_enq_ref_name, v_orign_name, v_pay_ref, v_orign_cbn_code, v_orign_kyc_level, v_ben_kyc_level, v_tran_loc, v_coll_acc_no, v_orign_acc_no, v_dest_inst_bank_code);
                      
                          INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
                          VALUES (v_session_id, CURRENT_TIMESTAMP, 'User account number does not exist. AccountNumber:"'||v_cr_account||'"', 'CREATE', 'DISPUTE - WEBHOOK TRANSACTION');
                       END IF;
                   ELSE 
                          INSERT INTO sw_transaction_dispute_fdlty_log(request_ref, request_typ, requester, status, provider, transaction_ref, transaction_desc, transaction_type, customer_ref, customer_email, customer_surname, customer_firstname, customer_mobile_no, tag, amount, from_bank_code, from_bank_name, tran_date, bank_code, cr_account, created_on, narration, session_id, channel_code, charge_amount, cr_account_name, originator_bvn, account_number, beneficiary_bvn, name_enquiry_ref, originator_name, payment_reference, originator_cbn_code, originator_kyc_level, beneficiary_kyc_level, transaction_location, collection_account_number, originator_account_number, destination_institution_bank_code) 
                          VALUES (v_req_ref, v_req_type, v_requester, v_status, v_provider, v_trx_ref, v_trx_desc, v_trx_type, v_cust_ref, v_cust_email, v_cust_surname, v_cust_fname, v_cust_mobile_no, v_tag, v_amt, v_from_bnk_code, v_bnk_name, CURRENT_TIMESTAMP, v_bnk_code, v_cr_account, CURRENT_TIMESTAMP, v_narration, v_session_id, v_channel_code, v_charge_amt, v_cr_acc_name, v_orign_bvn, v_acc_no, v_benef_bvn, v_enq_ref_name, v_orign_name, v_pay_ref, v_orign_cbn_code, v_orign_kyc_level, v_ben_kyc_level, v_tran_loc, v_coll_acc_no, v_orign_acc_no, v_dest_inst_bank_code);
                      
                          INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
                          VALUES (v_session_id, CURRENT_TIMESTAMP, 'Suspected duplicate transaction details in sw_transaction_fdlty_log. TransactionRef:"'||v_trx_ref||'"', 'CREATE', 'DISPUTE - WEBHOOK TRANSACTION');
                   END IF;
	           ELSE  
	               INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
                   VALUES (v_session_id, CURRENT_TIMESTAMP, 'Suspected duplicate transaction details in w_transaction_dispute_fdlty_log. TransactionRef:"'||v_trx_ref||'"', 'CREATE', 'DISPUTE - WEBHOOK TRANSACTION');
               END IF;	           
	       END IF;
   EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_session_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_create_txn_fdlty"}',
        'Error Parameters:' || v_session_id, 'WEBHOOK TRANSACTION');
END;$procedure$;

--
-- Procedure structure for proc_create_txn_disburse
--
CREATE OR REPLACE PROCEDURE proc_create_txn_disburse(v_profile_id varchar, v_cust_ref varchar, v_from_wallet_num varchar, v_tnx_amt float, v_txn_fee float, v_conv_fee float, v_pay_id varchar, v_txn_ref varchar, v_status varchar, v_message varchar, v_narration varchar, v_prov_resp_code varchar, v_tnx_final_amt float, v_orgin_acc_name varchar, v_orgin_acc_no varchar, v_ben_acc_no varchar, v_ben_acc_name varchar, v_den_inst_code varchar, INOUT flag text)
 LANGUAGE plpgsql
 AS $procedure$

  DECLARE v_from_wallet_id         sw_wallet_mast.wallet_id%TYPE;
  DECLARE v_current_bal            sw_wallet_details.balance%TYPE;
  DECLARE n_current_bal            sw_wallet_details.balance%TYPE;
  DECLARE n_fee                    sw_transaction_disburse_log.fee%TYPE;
  DECLARE n_amount                 sw_transaction_disburse_log.amount%TYPE;

  DECLARE v_err_mesg               varchar(32767);
  DECLARE v_err_state              varchar(32767);

  DECLARE v_result_json            TEXT; 
 
	BEGIN
          IF LOWER(v_status) ='failed' THEN 
          
               n_fee := (v_txn_fee + v_conv_fee);
              
               INSERT INTO sw_transaction_disburse_log(disburse_date, profile_id, customer_ref, amount, fee, payment_id, reference, status, message, narration, provider_response_code, final_amount, originator_account_name, originator_account_no, beneficiary_account_no, beneficiary_account_name, destination_instite_code)
               VALUES (CURRENT_TIMESTAMP, v_profile_id, v_cust_ref, v_tnx_amt, n_fee, v_pay_id, v_txn_ref, v_status, v_message, v_narration, v_prov_resp_code, v_tnx_final_amt, v_orgin_acc_name, v_orgin_acc_no, v_ben_acc_no, v_ben_acc_name, v_den_inst_code);
                      
               INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
               VALUES (v_profile_id, CURRENT_TIMESTAMP, 'Customer transaction failed. TransactionRef:"'||v_txn_ref||'"', 'CREATE', 'DISBURSEMENT');
               
               v_result_json := '{"status":"' || v_status || '","message":"Customer transaction failed from provider.","error":"' || v_prov_resp_code || '"}';
       
               --Return param
               flag :=v_result_json;
          ELSE 
               SELECT wallet_id INTO v_from_wallet_id FROM sw_wallet_mast WHERE wallet_number = v_from_wallet_num;
               SELECT balance INTO v_current_bal FROM sw_wallet_details WHERE wallet_id = v_from_wallet_id;
              
               n_current_bal := (v_current_bal - v_tnx_amt);
              
               n_fee := (v_txn_fee + v_conv_fee);
        	   n_amount := (v_tnx_amt - n_fee);
        	  
        	   INSERT INTO sw_transaction_disburse_log(disburse_date, profile_id, customer_ref, amount, fee, payment_id, reference, status, message, narration, provider_response_code, final_amount, originator_account_name, originator_account_no, beneficiary_account_no, beneficiary_account_name, destination_instite_code)
               VALUES (CURRENT_TIMESTAMP, v_profile_id, v_cust_ref, v_tnx_amt, n_fee, v_pay_id, v_txn_ref, v_status, v_message, v_narration, v_prov_resp_code, v_tnx_final_amt, v_orgin_acc_name, v_orgin_acc_no, v_ben_acc_no, v_ben_acc_name, v_den_inst_code);
        	   
        	   INSERT INTO sw_wallet_balance(wallet_id, reference_id, card_num, cr, dr, balance, last_update_date)
               VALUES (v_from_wallet_id, v_txn_ref, v_ben_acc_no, 0, v_tnx_amt, n_current_bal, CURRENT_TIMESTAMP);
               
               UPDATE sw_wallet_details
                  SET balance = n_current_bal,
                      last_update_date = CURRENT_TIMESTAMP
                WHERE wallet_id = v_from_wallet_id;
                      
                INSERT INTO sw_audit_trail(profile_id, audit_date, action_performed, event, entity) 
                VALUES (v_profile_id, CURRENT_TIMESTAMP, 'User debited successfully. TransactionRef:"'||v_txn_ref||'"', 'CREATE', 'DISBURSEMENT');
               
                v_result_json := '{"status":"OK","message":"User debited successfully","TxnRef":"'||v_txn_ref||'"}';
       
                --Return param
                flag :=v_result_json;
          END IF; 
   EXCEPTION
 	    WHEN OTHERS THEN
        v_err_mesg := sqlerrm;
        v_err_state := sqlstate;
        INSERT INTO sw_exception_log (profile_id,error_log_date,error_message,error_param,entity) 
        VALUES (v_profile_id,CURRENT_TIMESTAMP,'{"status":"Failed","error_name":"' || v_err_mesg || '","error_code":"' || v_err_state || '","functions":"proc_create_txn_disburse"}',
        'Error Parameters:' || v_profile_id, 'DISBURSEMENT');
        v_result_json := '{"status":"Failed","message":"Unexpected Server Error"}';
       
        --Return param
        flag :=v_result_json;
       
 flag :=v_result_json;
END;$procedure$;
