INTERACTIVE=true
SETUP_IDENTITY=false
while true; do
  case "$1" in
    -n | --non-interactive ) INTERACTIVE=false; shift ;;
    --identity ) SETUP_IDENTITY=true; shift ;;

    -- ) shift; break ;;
    * ) break ;;
  esac
done

[[ $SETUP_IDENTITY == true ]] && ( bash scripts/setup-certs.sh; bash scripts/setup-identity.sh )